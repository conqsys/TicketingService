using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface IWorklistResultTypeRepository : IDepRepository
    {
        Task<IEnumerable<IWorklistResultType>> FindAll();
    }
}
