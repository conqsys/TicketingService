using Ticket.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Ticket.Base.Entities;
using Ticket.DataAccess.Security;
using Ticket.BusinessLogic.Common;
using Npgsql;
using Ticket.Base.Repositories;

namespace Ticket.BusinessLogic.Security
{
    public class UserAuthenticationRepository<TUserAuthentication> : ModuleBaseRepository<TUserAuthentication>, IUserAuthenticationRepository where TUserAuthentication : class, IUserAuthentication, new()
    {
        private CodeGenerator _codeGenerator;
        private IUserRepository _userRepository;

        public UserAuthenticationRepository(BaseValidationErrorCodes errorCodes, DatabaseContext dbContext, IUser loggedUser,
            CodeGenerator codeGenerator,
            IUserRepository userRepository) 
            : base(errorCodes, dbContext, loggedUser)
        {
            this._codeGenerator = codeGenerator;
            this._userRepository = userRepository;
        }

        public async Task<bool> AddNew(long userId, string ipAddress, string email, string phone)
        {
            try
            {
                UserAuthentication userAuthentication = new UserAuthentication();
                userAuthentication.EmailAddress = email;
                userAuthentication.IpAddress = ipAddress;
                userAuthentication.PhoneNumber = phone;
                userAuthentication.UserId = userId;
                userAuthentication.VerificationCode = "";

                await this.AddNew(userAuthentication);
                return true;
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

        public async Task<bool> VerifiyAuthentication(long userId, string ipAddress, string verfifiedCode)
        {
            try
            {
                bool result = false;
                ICollection<ValidationCodeResult> validationErrors = new List<ValidationCodeResult>();

                var userAuthentication = await this.FindByIPAddress(ipAddress, userId) as TUserAuthentication;
                if (userAuthentication == null)
                {
                    await this.ThrowEntityException(new ValidationCodeResult(ErrorCodes[EnumErrorCode.RecordDoesNotExist]));
                }

                if (userAuthentication.VerificationCode == null || (userAuthentication.VerificationCode.ToLower() != verfifiedCode.ToLower()))
                {
                    await this.ThrowEntityException(new ValidationCodeResult(ErrorCodes[EnumErrorCode.VerificationCodeIsWrong]));
                }

                double totalMinutes = DateTime.Now.Subtract(userAuthentication.CodeExpiration.GetValueOrDefault(DateTime.Now)).TotalMinutes;
                if (totalMinutes > 10)
                {
                    await this.ThrowEntityException(new ValidationCodeResult(ErrorCodes[EnumErrorCode.VerificationTimeExpired]));
                }

                userAuthentication.Verified = true;
                await base.Update(userAuthentication, x => new
                {
                    x.Verified
                });
                result = true;

                return result;
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
        public async Task<IUserAuthentication> AddNew(IUserAuthentication entity)
        {
            TUserAuthentication tEntity = entity as TUserAuthentication;

            var errors = await this.ValidateEntityToAdd(tEntity);
            if (errors.Count() > 0)
                await this.ThrowEntityException(errors);

            try
            {
                this.StartTransaction();
                entity.Verified = false;
                entity.VerificationCode = _codeGenerator.GenerateCode();
                entity.CodeExpiration = DateTime.Now;
                var savedEntity = await base.AddNew(tEntity);
                this.CommitTransaction();
                /*write the email sending code*/

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

        public async Task<bool> Update(IUserAuthentication entity)
        {
            return true;
        }
        
        public override async Task<IEnumerable<IValidationResult>> ValidateEntityToAdd(TUserAuthentication entity)
        {
            ICollection<IValidationResult> errors = (await this.ValidateEntity(entity)).ToList();

            var userAuthentication = await this.FindByIPAddress(entity.IpAddress, entity.UserId.Value);
            if (userAuthentication != null)
            {
                var user = await this._userRepository.FindById(entity.UserId.GetValueOrDefault(0)) as IUser;
                errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.AlreadyAuthenticated, user.Email, entity.IpAddress]));
            }
            return errors;
        }

        public async Task<IUserAuthentication> FindByIPAddress(string ipAddress, long userId)
        {
            return await this.Connection.FirstOrDefaultAsync<TUserAuthentication>(i => i.UserId == userId && i.IpAddress == ipAddress);
        }

        public async Task<bool> ValidateIPAddress(string ipAddress, long userId)
        {
            var userAuthentication = await this.FindByIPAddress(ipAddress, userId);
            if (userAuthentication == null)
            {
                await this.ThrowEntityException(new ValidationCodeResult(ErrorCodes[EnumErrorCode.NewIpAddress]));
            }
            return true;
        }

        public override async Task<IEnumerable<IValidationResult>> ValidateEntity(TUserAuthentication entity)
        {
            return await base.ValidateEntity(entity);
        }
    }
}
