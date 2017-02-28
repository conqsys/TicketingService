using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface ITestWorklist : IEntity, IStamp
    {
        long TestId { get; set; }

        long WorklistId { get; set; }

        long Sequence { get; set; }

        IWorklist WorkList { get; set; }
    }
}
