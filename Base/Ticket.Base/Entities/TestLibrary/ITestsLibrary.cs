using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface ITestsLibrary : IEntity, IStamp, ILabChild
    {
        string Name { get; set; }
        string TestCode { get; set; }
        string Description { get; set; }
        long Estimatedtat { get; set; }
        string Cpt { get; set; }
        string Icd { get; set; }
        string Loinc { get; set; }
        bool Dna { get; set; }
        bool Rna { get; set; }
        int Specimenrepeat { get; set; }
        long RejectionId { get; set; }
        int ControlRepeat { get; set; }
        int CalliberRepeat { get; set; }
        long TestType { get; set; }
        long NucleicExtractionTest { get; set; }
        bool? IsNucleicTest { get; set; }
        bool Enabled { get; set; }

        IEnumerable<IWorklist> WorkLists { get; set; }
        
        IEnumerable<ITestWorklistReagent> Reagents { get; set; }

        string TestTypeDescription { get; set; }
    }
}
