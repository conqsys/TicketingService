
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
    public class TicketStatusRepository<TTicketStatus> : ModuleBaseRepository<TTicketStatus>, ITicketStatusRepository
        where TTicketStatus : class, ITicketStatus, new()
    {
        public TicketStatusRepository(BaseValidationErrorCodes errorCodes, DatabaseContext dbContext, IUser loggedUser)
            : base(errorCodes, dbContext, loggedUser)
        {
        }


        public async Task<ITicketStatus> AddNew(ITicketStatus entity)
        {
            try
            {
                this.StartTransaction();
                var savedEntity = await base.AddNew(entity as TTicketStatus);
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

        public async Task<ITicketStatus> Update(ITicketStatus entity)
        {
            try
            {
                TTicketStatus tEntity = entity as TTicketStatus;

                var errors = await this.ValidateEntityToUpdate(tEntity);
                if (errors.Count() > 0)
                    await this.ThrowEntityException(errors);


                this.StartTransaction();
                await base.Update(tEntity, x => new
                {
                    x.Name,
                    x.Description,
                    x.Enabled,
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

        public async Task<ITicketStatus> ChangeActiveState(long ticketStatusId, bool isEnabled, long modifiedById)
        {
            try
            {
                var ticketStatus = await base.FindById(ticketStatusId) as TTicketStatus;
                if (ticketStatus != null)
                {
                    ticketStatus.Enabled = isEnabled;
                    await base.Update(ticketStatus, x => new
                    {
                        x.Enabled,
                        x.ModifiedBy,
                        x.ModifiedDate
                    });
                }
                return ticketStatus;
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


        public async Task<IEnumerable<ITicketStatus>> FindAllTicketStatus()
        {
            return await this.Connection.SelectAsync<TTicketStatus>();
        }

    }
}
