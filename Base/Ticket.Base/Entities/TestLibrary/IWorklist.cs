using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IWorklist : IEntity, IStamp, ILabChild
    {
        string Name { get; set; }
        string Description { get; set; }
        long Estimatedtat { get; set; }
        long RejectionId { get; set; }
        long ProcedureId { get; set; }
        bool DnaRnaInfo { get; set; }
        long NaReactionId { get; set; }
        long WorklistTypeId { get; set; }
        bool Enabled { get; set; }
    }
}
