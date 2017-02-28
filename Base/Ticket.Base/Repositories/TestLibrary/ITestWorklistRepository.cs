using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface ITestWorklistRepository : IDepRepository
    {
        Task<ITestWorklist> Save(ITestWorklist entity);

        Task<ITestWorklist> AddNew(ITestWorklist entity);

        Task<bool> Update(ITestWorklist entity);
        
        Task<int> Delete(long id);

        Task<IEnumerable<ITestWorklist>> FindAllByTestId(long testId);

        Task<IEnumerable<IWorklist>> FindWorklistByTestId(long testId);

        Task CheckWorklistsAreInUse(long procedureId);

        Task CheckTestWorklistByWorklistId(long worklistId);

        Task DeleteTestWorklistByWorklistId(long worklistId);
    }
}
