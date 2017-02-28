using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface ITestReagent
    {
        long ReagentId { get; set; }

        double Amount { get; set; }

        long TestId { get; set; }
    }
}
