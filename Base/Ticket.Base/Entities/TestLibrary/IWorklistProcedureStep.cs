using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IWorklistProcedureStep : IEntity, IStamp
    {
        long ProcedureId { get; set; }

        int StepId { get; set; }

        string Description { get; set; }
    }
}
