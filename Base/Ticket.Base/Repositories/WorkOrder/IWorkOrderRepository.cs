using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Ticket.Base.Repositories
{
    public interface IWorkOrderRepository : IDepRepository
    {
        Task<IWorkOrder> AddNew(IWorkOrder entity);

        Task<IWorkOrder> Update(IWorkOrder entity);

        Task<IEnumerable<IWorkOrder>> FindAllWorkOrders();
    }
}
