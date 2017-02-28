using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface IWorklistRepository : IDepRepository
    {
        Task<IWorklist> Save(IWorklist entity);

        Task<IWorklist> AddNew(IWorklist entity);

        Task<bool> Update(IWorklist entity);

        Task<IPaginationQuery<IWorklist>> FindAllByWorklistType(long worklistType, int? startPageNo, int? endPageNo, int? pageSize, bool runCount);

        Task<IPaginationQuery<IWorklist>> FindAllBySearchParameter(string name, string description, string worklistType, int? startPageNo, int? endPageNo, int? pageSize, bool runCount);

        Task<IPaginationQuery<IWorklist>> FindAllByLabId(int? startPageNo, int? endPageNo, int? pageSize, bool runCount);

        Task CheckNAReactionInWorklist(long naReactionId);

        Task IsRejectionExistInWorklist(long rejectionId);

        Task<bool> HasPermission(long id);

        Task<bool> HasAnyActiveOrPendingCase(long id);

        Task<bool> Delete(long id);

        Task<bool> ChangeStatus(long id, bool isActive);

        Task CheckWorklistByWorklistType(long worklistTypeId);
    }
}
