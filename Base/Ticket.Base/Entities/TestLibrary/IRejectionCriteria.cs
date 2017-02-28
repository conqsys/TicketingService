using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IRejectionCriteria : IEntity, IStamp, ILabChild
    {
        string Name { get; set; }
        string Description { get; set; }
        double SpecimenQuantity { get; set; }
        double SpecimenTemperature { get; set; }
        long SpecimenProcessingTime { get; set; }
    }
}
