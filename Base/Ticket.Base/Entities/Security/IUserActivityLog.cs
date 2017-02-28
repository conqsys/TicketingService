using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IUserActivityLog : IEntity
    {
        DateTime? TimeLogged { get; set; }

        string CaseNo { get; set; }

        long? LabId { get; set; }

        long? UserId { get; set; }

        long? ModifiedUserId { get; set; }
    }
}
