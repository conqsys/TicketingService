using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface ITestWorklistReagentRepository : IDepRepository
    {
        Task<ITestWorklistReagent> Save(ITestWorklistReagent entity);

        Task<ITestWorklistReagent> AddNew(ITestWorklistReagent entity);

        Task<bool> Update(ITestWorklistReagent entity);
        
        Task<int> Delete(long id);

        Task<IEnumerable<ITestWorklistReagent>> FindByTestWorklistId(long testWorklistId);
    }
}
