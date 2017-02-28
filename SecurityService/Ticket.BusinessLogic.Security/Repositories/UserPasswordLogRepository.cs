using Ticket.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Ticket.Base.Entities;
using Ticket.DataAccess.Security;
using Npgsql;
using System.Linq.Expressions;
using Ticket.Base.Repositories;
using Ticket.BusinessLogic.Common;

namespace Ticket.BusinessLogic.Security
{
    public class UserPasswordLogRepository<TUserPasswordLog, TUser> : ModuleBaseRepository<TUserPasswordLog>, IUserPasswordLogRepository
        where TUserPasswordLog : class, IUserPasswordLog, new()
        where TUser : class, IUser, new()
    {
        private EncryptionProvider _encryptionProvider;

        public UserPasswordLogRepository(BaseValidationErrorCodes errorCodes, DatabaseContext dbContext, IUser loggedUser,
            EncryptionProvider encryptionProvider) 
            : base(errorCodes, dbContext, loggedUser)
        {
            this._encryptionProvider = encryptionProvider;
        }

        public async Task<IUserPasswordLog> AddNew(IUserPasswordLog entity)
        {
            TUserPasswordLog tEntity = entity as TUserPasswordLog;

            var errors = await this.ValidateEntityToAdd(tEntity);
            if (errors.Count() > 0)
                await this.ThrowEntityException(errors);

            try
            {
                this.StartTransaction();
                var savedEntity = await base.AddNew(tEntity);
                this.CommitTransaction();

                return savedEntity;
            }
            catch (PostgresException ex)
            {
                throw new EntityUpdateException(ex);
            }
            catch
            {
                throw;
            }
        }
        
        public override async Task<IEnumerable<IValidationResult>> ValidateEntityToAdd(TUserPasswordLog entity)
        {
            ICollection<IValidationResult> errors = (await this.ValidateEntity(entity)).ToList();

            IEnumerable<IUserPasswordLog> existingReocrds = (await this.Connection
                .SelectAsync<TUserPasswordLog>(i => i.UserId == entity.UserId))
                .OrderByDescending(i => i.DateChanged)
                .Take(5);

            if (existingReocrds != null)
            {
                var checkvalue = existingReocrds.FirstOrDefault(i => i.PreviousPassword == entity.PreviousPassword);
                if (checkvalue != null)
                {
                    errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.PasswordUsedBefore]));
                }
            }
            return errors;
        }

        public override async Task<IEnumerable<IValidationResult>> ValidateEntity(TUserPasswordLog entity)
        {
            return await base.ValidateEntity(entity);
        }

        public async Task<bool> ValidateUserPassword(long userId, string password)
        {
            ICollection<ValidationCodeResult> errors = new List<ValidationCodeResult>();

            string encryptedPassword = _encryptionProvider.Encrypt(password);

            var requestedtuser = await this.Connection.FirstOrDefaultAsync<TUser>(i => i.Id == userId);
            if (requestedtuser == null)
            {
                errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.UserIdDoesNotExists, userId.ToString()]));
            }
            else
            {
                if (requestedtuser.Password == encryptedPassword)
                {
                    errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.OldAndNewPwdNotSame]));
                }
            }

            /* throw error password is same or user does not exist, does not make sense to validate further */
            if (errors.Count > 0)
                await this.ThrowEntityException(errors);
            
            /* checked again if still an error */
            if (errors.Count > 0)
                await this.ThrowEntityException(errors);

            var existingReocrd = (await this.Connection
                                      .SelectAsync<IUserPasswordLog>(i => i.UserId == userId))
                                     .OrderByDescending(i => i.DateChanged)
                                     .Take(5)
                                     .FirstOrDefault(i => i.PreviousPassword == encryptedPassword);

            if (existingReocrd != null)
            {
                errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.PasswordUsedBefore]));
            }

            if (errors.Count > 0)
            {
                await this.ThrowEntityException(errors);
            }
            return true;
        }
    }
}
