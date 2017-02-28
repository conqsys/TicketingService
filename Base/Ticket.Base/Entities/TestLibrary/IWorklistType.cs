using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IWorklistType : IEntity, IStamp, ILabChild
    {
        string Name { get; set; }
    }
}
