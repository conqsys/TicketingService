using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface IWorklistScheduleRepository : IDepRepository
    {
        Task<IWorklistSchedule> Save(IWorklistSchedule entity);

        Task<IWorklistSchedule> AddNew(IWorklistSchedule entity);

        Task<bool> Update(IWorklistSchedule entity);
        
        Task<IEnumerable<IWorklistSchedule>> FindAllByWorklistId(long worklistId);

        Task<int> Delete(long id);

        Task DeleteWorklistScheduleByWorklistId(long id);

        Task<IEnumerable<IWorklistSchedule>> FindWorklistScheduleByWorklistId(long worklistId);
    }
}
