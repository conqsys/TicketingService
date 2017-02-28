using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IReflexTest : IEntity, IStamp
    {
        long TestId { get; set; }

        long TestResult { get; set; }

        long OrderTestId { get; set; }
        
        long ReflexTestId { get; set; }

        string ReflexTestName { get; set; }

        string TestStatus { get; set; }
    }
}
