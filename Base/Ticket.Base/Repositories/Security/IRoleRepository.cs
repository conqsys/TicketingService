using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface IRoleRepository : IDepRepository
    {
        Task<IRole> AddNew(IRole entity);

        Task<bool> Update(IRole entity);
        
        Task<IPaginationQuery<IRole>> FindAllRoles(int? startPageNo, int? endPageNo, int? pageSize, bool runCount);

        Task Delete(long id);
    }
}
