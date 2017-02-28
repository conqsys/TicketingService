using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface ITestProfileDetail : IEntity, IStamp
    {
        long ProfileId { get; set; }

        long TestId { get; set; }
    }
}
