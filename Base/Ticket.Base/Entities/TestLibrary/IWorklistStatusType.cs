using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IWorklistStatusType : IEntity, IStamp, ILabChild
    {
        string Name { get; set; }
    }
}
