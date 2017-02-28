using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.API.Common
{
    public class IdentityPermission : IPermission
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
