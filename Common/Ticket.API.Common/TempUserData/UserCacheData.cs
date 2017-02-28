using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.API.Common
{
    public class UserCacheData
    {
        public UserCacheData()
        {
            InitialIze();
        }

        private Dictionary<string, IUser> _cachedObject;

        public bool Contains(string key )
        {
            return this._cachedObject.ContainsKey(key);
        }

        public IUser this[string key]
        {
            get
            {
                return this._cachedObject[key];
            }
        }
        private void InitialIze()
        {
            IdentityUser user = new IdentityUser(null, null);
            user.Id = 2;
            //user.LabId = 1;
            //user.Username = "rahul";
            user.Email = "test@test.com";
          //  user.Roles = new List<IRole>();

            IdentityRole identityRole = new IdentityRole();
            identityRole.Id = 10;
            identityRole.LabId = 1;
            identityRole.RoleType = "Admin";

            identityRole.Permissions = new List<IPermission>();
            identityRole.Permissions.Add(new IdentityPermission()
            {
                Action = "Add",
                Description = "Add",
                Id = 1
            });

           // user.Roles.Add(identityRole);
                
            IdentityRole identityRole2 = new IdentityRole();
            identityRole2.Id = 11;
            identityRole2.LabId = 1;
            identityRole2.RoleType = "SuperAdmin";

           // user.Roles.Add(identityRole2);

            this._cachedObject = new Dictionary<string, IUser>();
            this._cachedObject.Add("2", user);

        }

    }
}
