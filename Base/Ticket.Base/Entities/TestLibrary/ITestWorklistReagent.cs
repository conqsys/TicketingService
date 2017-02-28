using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface ITestWorklistReagent : IEntity, IStamp
    {
        long ReagentId { get; set; }

        double Amount { get; set; }

        long TestWorklistId { get; set; }
    }
}
