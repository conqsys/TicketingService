using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface ILabContact : IVersioned, IStamp
    {
        string ContactPersonEmail { get; set; }

        string ContactPersonFax { get; set; }

        string ContactPersonPhone { get; set; }

        long LabId { get; set; }

        string ContactName { get; set; }
    }
}
