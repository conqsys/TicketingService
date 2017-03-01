using Ticket.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;
using Ticket.Base.Entities;
using Ticket.Base.Repositories;
using Ticket.DataAccess.Security;
using Npgsql;
using SimpleStack.Orm;
using SimpleStack.Orm.Expressions;
using Ticket.BusinessLogic.Common;
using Ticket.Base;
using Ticket.Base.Objects;
namespace Ticket.BusinessLogic.Security
{
    public class UserRepository<TUser , TRole , TClient> : ModuleBaseRepository<TUser>, IUserRepository 
        where TUser : class, IUser, new()
        where TRole : class, IRole, new()
         where TClient : class, IClient, new()
    {
        private EncryptionProvider _encryptionProvider;
        private IGroupUserRepository _groupUserRepository;
        public UserRepository(BaseValidationErrorCodes errorCodes,
            DatabaseContext dbContext, 
            IUser loggedUser,
            EncryptionProvider encryptionProvider,
            IGroupUserRepository groupUserRepository
           ) 
            : base(errorCodes, dbContext, loggedUser)
        {
            this._encryptionProvider = encryptionProvider;
            this._groupUserRepository = groupUserRepository;
        }

        public async Task<IUser> CreateInstance()
        {
            return new User();
        }

        public async Task<IUser> AddNew(IUser entity)
        {
            TUser tEntity = entity as TUser;

            var errors = await this.ValidateEntityToAdd(tEntity);
            if (errors.Count() > 0)
                await this.ThrowEntityException(errors);

            try
            {
                this.StartTransaction();
                tEntity.CreatedDate = DateTime.Now;
                 tEntity.CreatedBy = LoggedUser.Id;
                if (tEntity.Password != "" && tEntity.Password != null) {
                    tEntity.Password = _encryptionProvider.Encrypt(tEntity.Password);
                }

                if (tEntity.RoleId == 1) { //ONshore
                    tEntity.AccountId = null;
                    tEntity.CoordinatorId = null;
                }
                else if (tEntity.RoleId == 2) {//OFFshore
                    tEntity.AccountId = null;
                } else if (tEntity.RoleId == 3) {//Client
                    tEntity.CoordinatorId = null;
                }

                var savedEntity = await base.AddNew(tEntity);
                long userId = savedEntity.Id;
                await this._groupUserRepository.DeleteGroupUserByUserId(userId);
                await this.SaveGroupUser(tEntity, userId);
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

        public async Task<bool> SaveGroupUser(IUser tEntity, long userId) {

            if (tEntity.RoleId == 1)
            { 
                foreach (var user in tEntity.GroupUsers) {

                    IGroupUser groupUser = user;
                    groupUser.Id = 0;
                    groupUser.UserId = userId;
                    groupUser.GroupId = user.Id;
                    var savedUser = await this._groupUserRepository.AddNew(user);
                }
            }
            return true;
        }


        public async Task<IUser> CheckLogin(string userName, string password)
        {
            return await this.Connection.FirstOrDefaultAsync<TUser>(i => i.Email == userName && i.Password == _encryptionProvider.Encrypt(password));
        }

        public async Task IncreaseAttempts(string userName)
        {
            var user = await this.Connection.FirstOrDefaultAsync<TUser>(i => i.Email == userName);
            if (user != null)
            {
                try
                {
                   // user.AuthAttempts = user.AuthAttempts + 1;
                    await this.UpdateAuthAttempts(user);
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
        }

        private async Task UpdateAuthAttempts(TUser user)
        {
            await base.Update(user, x => new
            {
               // x.AuthAttempts,
                x.ModifiedBy,
                x.ModifiedDate
            });
        }
        
        public async Task<bool> CheckAuthentication(string userName, string ipAddress)
        {
            ICollection<ValidationCodeResult> errors = new List<ValidationCodeResult>();

            try
            {
                var currentUser = await this.FindFirstByName(userName);
                if (currentUser == null)
                {
                    /* nothing will be checked if no user exists for the given username*/
                    await this.ThrowEntityException(new ValidationCodeResult(ErrorCodes[EnumErrorCode.IncorrectUserName]));
                }

                if (errors.Count > 0)
                    await this.ThrowEntityException(errors);
                
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
        
        public async Task<IUser> LogOut(long userId)
        {
            try
            {
                var user = await base.FindById(userId) as TUser;
                if (user != null)
                {
                   // user.AuthAttempts = 0;
                    await this.UpdateAuthAttempts(user);
                }
                return user;
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<IUser> UpdatePassword(long userId, string password)
        {
            var user = await base.FindById(userId) as TUser;

            user.Password = _encryptionProvider.Encrypt(password);
           // user.DatePassword = DateTime.Now.AddDays(90);

            await base.Update(user, x => new
            {
                x.Password,
               // x.DatePassword,
                x.ModifiedBy,
                x.ModifiedDate
            });
            return user;
        }

        public async Task<IUser> Update(IUser entity)
        {
            try
            {
                TUser tEntity = entity as TUser;

                TUser user = await base.FindById(entity.Id) as TUser;
                var errors = await this.ValidateEntityToUpdate(tEntity);
                if (errors.Count() > 0)
                    await this.ThrowEntityException(errors);

                this.StartTransaction();
                tEntity.ModifiedBy = LoggedUser.Id;
                tEntity.ModifiedDate = DateTime.Now;
                if (tEntity.Password != "" && tEntity.Password != null)
                {
                    tEntity.Password = _encryptionProvider.Encrypt(tEntity.Password);
                }
                if (tEntity.RoleId == 1)
                { //ONshore
                    tEntity.AccountId = null;
                    tEntity.CoordinatorId = null;
                }
                else if (tEntity.RoleId == 2)
                {//OFFshore
                    tEntity.AccountId = null;
                }
                else if (tEntity.RoleId == 3)
                {//Client
                    tEntity.CoordinatorId = null;
                }

                await base.Update(tEntity, x => new
                {
                    x.Email,
                    x.Enabled,
                    x.UserName,
                    x.RoleId,
                    x.CoordinatorId,
                    x.AccountId,
                    x.ModifiedBy,
                    x.ModifiedDate
                });
                await this._groupUserRepository.DeleteGroupUserByUserId(entity.Id);
                await this.SaveGroupUser(tEntity, tEntity.Id);
                this.CommitTransaction();

                return user;
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<IPaginationQuery<IUser>> FindAll(int? startPageNo, int? endPageNo, int? pageSize, bool runCount)
        {
            var result = await this.Connection.QueryPagination<TUser>(startPageNo, endPageNo, pageSize, runCount);

          //  var result = await this.Connection.QueryPagination<TUser>(i => i.LabId == LoggedUser.LabId, startPageNo, endPageNo, pageSize, runCount);
            return new PaginationQuery<IUser>
            {
                Data = result.Data,
                TotalRecords = result.TotalRecords
            };
        }

        public async Task<IEnumerable<IUser>> FindAllByRoleId(long roleId)
        {
            var users = await this.Connection.SelectAsync<TUser>(i => i.RoleId == roleId);
            return users;
        }


        public async Task<IUser> FindFirstByName(string userName)
        {
            var user = await this.Connection.FirstOrDefaultAsync<TUser>(i => i.Email == userName);
            return user;
        }

        public async Task<IUser> FindFirstByEmail(string email)
        {
            var user = await this.Connection.FirstOrDefaultAsync<TUser>(i => i.Email == email);
            return user;
        }

        public override async Task<bool> Exists(string email)
        {
            var existedUser = await this.FindFirstByEmail(email);
            return existedUser != null;
        }


        public async Task<IEnumerable<IValidationResult>> ValidateExists(string userName, ICollection<IValidationResult> errors)
        {
            var existedUser = await this.FindFirstByName(userName);
            if (existedUser != null)
            {
                errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.UserExists, userName]));
            }
            return errors;
        }

        public override async Task<IEnumerable<IValidationResult>> ValidateEntityToAdd(TUser entity)
        {
            ICollection<IValidationResult> errors = (await this.ValidateEntity(entity)).ToList();

            /* check if user already exists then same email, theow exception if true*/
            if (await this.Exists(entity.Email))
                errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.UserExists, entity.Email]));

            return errors;
        }

        public override async Task<IEnumerable<IValidationResult>> ValidateEntityToUpdate(TUser entity)
        {
            ICollection<IValidationResult> errors = (await this.ValidateEntity(entity)).ToList();

            var user = await base.FindById(entity.Id);
            if (user == null)
            {
                errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.UserDoesNotExist]));
            }

            return errors;
        }

        public async Task<IEnumerable<IValidationResult>> ValidateEntity(IUser entity)
        {
            ICollection<IValidationResult> errors = (await base.ValidateEntity(entity as TUser)).ToList();
            
            //if (entity.RoleIds != null && entity.RoleIds.Count == 0)
            //{
            //    errors.Add(new ValidationCodeResult(ErrorCodes[EnumErrorCode.NoRoleForAssigned, entity.Username]));
            //}
            return errors;
        }

        public async Task<IUser> ChangeActiveState(long userId, bool isEnabled, long modifiedById)
        {
            try
            {
                var user = await base.FindById(userId) as TUser;
                if (user != null)
                {
                    user.Enabled = isEnabled;
                    await base.Update(user, x => new
                    {
                        x.Enabled,
                        x.ModifiedBy,
                        x.ModifiedDate
                    });
                }
                return user;
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

        public override async Task<IEnumerable<IValidationResult>> ValidateEntityToUpdate(TUser entityNew, TUser entityDb)
        {
            var baseResults = await base.ValidateEntityToUpdate(entityNew, entityDb);
            var commonResults = await this.ValidateEntityToUpdate(entityNew);
            baseResults = commonResults.Union(baseResults);
            return baseResults;
        }

        public async Task<IEnumerable<IUser>> FindUserByAccountId(long accountId)
        {
            //List<IUserDTO> users = new List<IUserDTO>();
            var users = await this.Connection.SelectAsync<TUser>(i => i.AccountId == accountId);
            return users;

            //List<IUserDTO> users = new List<IUserDTO>();
            //var sqlQuery = new JoinSqlBuilder<TUser, TUser>(this.Connection.DialectProvider)
            //                     .Join<TUser, TUser>(i => i.Id, i => i.CoordinatorId)
            //                     .Join<TRole, TUser>(i => i.Id, i => i.RoleId)
            //                      .Join<TClient, TUser>(i => i.Id, i => i.AccountId)
            //                     .SelectAll<TUser>()
            //                     // .SelectAll<TClient>()
            //                     .Where<TUser>(ri => ri.AccountId == accountId)
            //                     .ToSql();

            //var result = await this.Connection.QueryAsync<TUser>(sqlQuery, new { accountId = accountId });
            //foreach (TUser obj in result)
            //{
            //    UserDTO user = new UserDTO();
            //    user.UserId = obj.Id;
            //    user.RoleId = obj.RoleId;
            //    user.RoleName = obj.RoleName;
            //    user.UserName = obj.UserName;
            //    user.UserEmail = obj.Email;
            //    user.AccountId = obj.AccountId;
            //    user.ClientName = obj.ClientName;
            //    user.CoordinatorId = obj.CoordinatorId;
            //    user.CoordinatorName = obj.CoordinatorName;
            //    users.Add(user);
            //}
            //return users;
        }

        public override async Task<object> FindById(long id)
        {
            var user = await base.FindById(id) as TUser;
            if (user != null)
            {
                IEnumerable<IGroupUser> groupUsers = await this._groupUserRepository.FindGroupUsersByUserId(id);
                if (user != null)
                {
                    user.GroupUsers = groupUsers;
                }
            }
            return user;
        }
    }
}