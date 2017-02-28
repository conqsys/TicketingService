using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.API.Common
{
    public class IdentityRole: IRole
    {
        public long Id { get; set; }
        public long Version { get; set; }
        public string Authority { get; set; }

        public string Description { get; set; }

        public string RoleType { get; set; }

        public List<IPermission> Permissions { get; set; }

        public List<long> PermissionIds { get; set; }

        public long? ModifiedBy { get; set; }

        public  DateTime? ModifiedDate { get; set; }

        public long LabId { get; set; }

        public long? CreatedBy { get; set; }

        public  DateTime? CreatedDate { get; set; }
         public bool Enabled { get; set; }
        public string Name { get; set; }

    }
}
