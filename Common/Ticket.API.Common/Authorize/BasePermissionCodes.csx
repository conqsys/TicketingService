using Ticket.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.API.Common
{

    public class BasePermissionCodes
    {
        public BasePermissionCodes(IPermissionRepository permissionRepoistory)
        {
            this.PermissionCodes = new Dictionary<long, string>();
            var allPermissions = ((IEnumerable<Base.Entities.IPermission>)permissionRepoistory.GetAllPermissions()).ToList();

            foreach (var item in allPermissions)
            {
                this.PermissionCodes.Add(item.Id, item.Action);
            }
        }


        public virtual void AddPermission(Int32 permissionId, string message)
        {
            this.PermissionCodes.Add(permissionId, message);
        }

        public virtual bool Contains(long permissionId)
        {
            return this.PermissionCodes.ContainsKey(permissionId);
        }

        public virtual string this[long permissionId]
        {
            get
            {
                return this.PermissionCodes[permissionId];
            }set
            {
                this.PermissionCodes[permissionId] = value;
            }
        }

        public Dictionary<long, string> PermissionCodes { get; set; }
    }
}
