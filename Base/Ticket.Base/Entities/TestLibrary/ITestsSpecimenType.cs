using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface ITestsSpecimenType : IEntity, IStamp
    {
        long TestId { get; set; }

        long MacroId { get; set; }
        
        string MacroName { get; set; }
    }
}
