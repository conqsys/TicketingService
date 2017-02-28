using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface INaReactionTemplate : IEntity, IStamp, ILabChild
    {
        string Name { get; set; }
        string Description { get; set; }
        bool Dna { get; set; }
        bool Rna { get; set; }
        long DnaSpecimenType { get; set; }
        double A260A230 { get; set; }
        double A260A280 { get; set; }
        double DilutionFactor { get; set; }
        double ConcentrationUnit { get; set; }
        double CalculationCoefficient { get; set; }
    }
}
