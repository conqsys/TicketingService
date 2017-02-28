
using Dapper;
using Ticket.Base;
using Ticket.Base.Entities;
using Ticket.Base.Repositories;
using Ticket.DataAccess.TicketService;
using Ticket.DataAccess.Common;
using Npgsql;
using SimpleStack.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ticket.BusinessLogic.Common;
using Ticket.Base.Objects;

namespace Ticket.BusinessLogic.TicketService
{
    public class ClientRepository<TClient> : ModuleBaseRepository<TClient>, IClientRepository
        where TClient : class, IClient, new()
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private EncryptionProvider _encryptionProvider;
        public ClientRepository(BaseValidationErrorCodes errorCodes,
                                                        DatabaseContext dbContext,
                                                        IUser loggedUser,
                                                        IUserRepository userRepository,
                                                        IRoleRepository roleRepository,
                                                         EncryptionProvider encryptionProvider)
            : base(errorCodes, dbContext, loggedUser)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._encryptionProvider = encryptionProvider;
        }


        public async Task<IClient> AddNew(IClient entity)
        {
            try
            {
                this.StartTransaction();
                TClient tEntity = entity as TClient;
                var savedEntity = await base.AddNew(entity as TClient);

                ////get role(client) -1 
                //if (savedEntity.LoginEnabled == true)
                //{
                //    IUser user = await this._userRepository.CreateInstance();
                //    string password = _encryptionProvider.GetRandomString();
                //    user.Id = 0;
                //    user.Enabled = true;
                //    user.UserName = tEntity.UserName;
                //    user.Email = tEntity.UserEmail;
                //    user.Password = _encryptionProvider.Encrypt(password);
                //    user.RoleId = 1;
                //    user.CoordinatorId = null;
                //    user.AccountId = savedEntity.Id;

                //    var savedUser = await this._userRepository.AddNew(user);
                //}
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

        public async Task<IClient> Update(IClient entity)
        {
            try
            {
                TClient tEntity = entity as TClient;

                var errors = await this.ValidateEntityToUpdate(tEntity);
                if (errors.Count() > 0)
                    await this.ThrowEntityException(errors);


                this.StartTransaction();
                await base.Update(tEntity, x => new
                {
                    x.ClientName,
                    x.Description,
                    x.Enabled,
                    x.LoginEnabled,
                    x.ClientAcronym,
                    x.RequestTypeId,
                    x.ModifiedBy,
                    x.ModifiedDate
                });

                this.CommitTransaction();
                return tEntity;
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

        public async Task<IClient> ChangeActiveState(long clientId, bool isEnabled, long modifiedById)
        {
            try
            {
                var client = await base.FindById(clientId) as TClient;
                if (client != null)
                {
                    client.Enabled = isEnabled;
                    await base.Update(client, x => new
                    {
                        x.Enabled,
                        x.ModifiedBy,
                        x.ModifiedDate
                    });
                }
                return client;
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

        public override async Task<object> FindById(long id)
        {
            var client = await base.FindById(id) as TClient;
            if (client != null)
            {
               IEnumerable<IUser> users = await this._userRepository.FindUserByAccountId(id);
                if (client != null)
                {
                    client.ClientUsers = users;
                }
            }
            return client;
        }


        public async Task<IEnumerable<IClient>> FindAllClients()
        {
            return await this.Connection.SelectAsync<TClient>();

        }

    }
}
