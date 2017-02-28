
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

namespace Ticket.BusinessLogic.TicketService
{
    public class WorkOrderRepository<TWorkOrder> : ModuleBaseRepository<TWorkOrder>, IWorkOrderRepository
        where TWorkOrder : class, IWorkOrder, new()
    {
        public WorkOrderRepository(BaseValidationErrorCodes errorCodes,
                                                        DatabaseContext dbContext,
                                                        IUser loggedUser,
                                                        IUserRepository userRepository,
                                                        IRoleRepository roleRepository,
                                                         EncryptionProvider encryptionProvider)
            : base(errorCodes, dbContext, loggedUser)
        {
        }


        public async Task<IWorkOrder> AddNew(IWorkOrder entity)
        {
            try
            {
                this.StartTransaction();
                TWorkOrder tEntity = entity as TWorkOrder;
                var savedEntity = await base.AddNew(entity as TWorkOrder);
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

        public async Task<IWorkOrder> Update(IWorkOrder entity)
        {
            try
            {
                TWorkOrder tEntity = entity as TWorkOrder;

                var errors = await this.ValidateEntityToUpdate(tEntity);
                if (errors.Count() > 0)
                    await this.ThrowEntityException(errors);


                this.StartTransaction();
                await base.Update(tEntity, x => new
                {
                    x.AssignedUserRoleId,
                    x.BatchName,
                    x.ScanDate,
                    x.PageNo,
                    x.ReferenceNo,
                    x.MRNo,
                    x.PatientName,
                    x.DOSDate,
                    x.Amount,
                    x.ClientDoctorName,
                    x.ReferingDoctorName,
                    x.Comments,
                    x.WorkOrderStatusId, 
                    x.RequestTypeId,
                    x.ProcessId,
                    x.FacilityId,
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

        public async Task<IEnumerable<IWorkOrder>> FindAllWorkOrders()
        {
            return await this.Connection.SelectAsync<TWorkOrder>();
        }

    }
}
