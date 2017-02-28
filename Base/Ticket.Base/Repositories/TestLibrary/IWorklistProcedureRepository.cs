using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface IWorklistProcedureRepository : IDepRepository
    {
        Task<IWorklistProcedure> Save(IWorklistProcedure entity);

        Task<IWorklistProcedure> AddNew(IWorklistProcedure entity);

        Task<bool> Update(IWorklistProcedure entity);
        
        Task<IPaginationQuery<IWorklistProcedure>> FindAllByLabId(int? startPageNo, int? endPageNo, int? pageSize, bool runCount);

        Task<int> Delete(long id);

        Task<bool> HasPermission(long id);
    }
}
