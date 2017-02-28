using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IUserPasswordLog : IEntity
    {
        long? UserId { get; set; }

        string PreviousPassword { get; set; }

        DateTime? DateChanged { get; set; }
    }
}
