
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

namespace Ticket.BusinessLogic.TicketService
{
    public class GroupUserRepository<TGroupUser> : ModuleBaseRepository<TGroupUser>, IGroupUserRepository
        where TGroupUser : class, IGroupUser, new()
    {
        public GroupUserRepository(BaseValidationErrorCodes errorCodes, DatabaseContext dbContext, IUser loggedUser)
            : base(errorCodes, dbContext, loggedUser)
        {
        }


        public async Task<IGroupUser> AddNew(IGroupUser entity)
        {
            try
            {
                this.StartTransaction();
                var savedEntity = await base.AddNew(entity as TGroupUser);
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

        public async Task<int> DeleteGroupUser(long id)
        {
            try
            {
                this.StartTransaction();
                int deletedRecords = await this.Connection.DeleteAllAsync<TGroupUser>(i => i.Id == id);
                this.CommitTransaction();
                return deletedRecords;
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
}
