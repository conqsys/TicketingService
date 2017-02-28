using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface IWorklistProcedureStepRepository : IDepRepository
    {
        Task<IWorklistProcedureStep> Save(IWorklistProcedureStep entity);

        Task<IWorklistProcedureStep> AddNew(IWorklistProcedureStep entity);

        Task<bool> Update(IWorklistProcedureStep entity);
        
        Task<IEnumerable<IWorklistProcedureStep>> FindAllByProcedureId(long procedureId);

        Task<int> Delete(long id);

        Task DeleteByProcedureId(long procedureId);
    }
}
