
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
    public class FacilityRepository<TFacility> : ModuleBaseRepository<TFacility>, IFacilityRepository
        where TFacility : class, IFacility, new()
    {
        public FacilityRepository(BaseValidationErrorCodes errorCodes, DatabaseContext dbContext, IUser loggedUser)
            : base(errorCodes, dbContext, loggedUser)
        {
        }


        public async Task<IFacility> AddNew(IFacility entity)
        {
            try
            {
                this.StartTransaction();
                var savedEntity = await base.AddNew(entity as TFacility);
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

        public async Task<IFacility> Update(IFacility entity)
        {
            try
            {
                TFacility tEntity = entity as TFacility;

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

        public async Task<IFacility> ChangeActiveState(long requestTypeId, bool isEnabled, long modifiedById)
        {
            try
            {
                var requestType = await base.FindById(requestTypeId) as TFacility;
                if (requestType != null)
                {
                    requestType.Enabled = isEnabled;
                    await base.Update(requestType, x => new
                    {
                        x.Enabled,
                        x.ModifiedBy,
                        x.ModifiedDate
                    });
                }
                return requestType;
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


        public async Task<IEnumerable<IFacility>> FindAllFacilities()
        {
            return await this.Connection.SelectAsync<TFacility>();
        }

    }
}
