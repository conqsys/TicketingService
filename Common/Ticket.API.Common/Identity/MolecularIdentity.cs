using Microsoft.AspNetCore.Http;
using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Ticket.API.Common
{
    public enum MolecularRole
    {
        LabUser,
        LabAdmin,
        SysAdmin
    }

    public class MolecularPrincipal : ClaimsPrincipal
    {
        public MolecularPrincipal(MolecularIdentity identity) : base()
        {
            this._identity = identity;
            identity.Principal = this;
            this.AddIdentity(identity);

        }
        public bool IsInRole(MolecularRole role)
        {
            bool result = base.IsInRole(role.ToString());
            return result;
        }
        private IIdentity _identity;
        public override IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }

    }

    public class MolecularIdentity : ClaimsIdentity
    {
        //
        public MolecularIdentity(IIdentity identity,
            IEnumerable<Claim> claims,
            string authenticationType,
            string nameType,
            string roleType,
            IUser user = null) : base(identity, claims, authenticationType, nameType, roleType)
        {
            if (user != null)
            {
                this.User = user;
                this.AddClaim(new Claim("Id", user.Id.ToString()));
            }
              
        }

        public MolecularPrincipal Principal { get; set; }

        public void AddUserClaims(IUser user)
        {
            this.AddClaim(new Claim("UserName", user.UserName));
            this.AddClaim(new Claim("Email", user.Email));
            //this.AddClaim(new Claim("LabId", user.LabId.ToString()));
            //this.AddClaim(new Claim("LabclientId", user.LabclientId.GetValueOrDefault(0).ToString()));
            //this.AddClaim(new Claim("UserType", user.UserType == null ? "" : user.UserType));

            //this.AddClaim(new Claim("Id", user.Id.ToString()));
            //this.AddClaim(new Claim("Email", user.Email == null ? "" : user.Email));
            //this.AddClaim(new Claim("Contact", user.Contact == null ? "" : user.Contact));


            //foreach (var role in this.User.Roles)
            //{
            //    this.AddClaim(new Claim(this.RoleClaimType, role.Description));
            //    foreach (var permission in role.Permissions)
            //    {
            //        this.AddClaim(new Claim(permission.Action, permission.Action));
            //    }
            //}
        }
        private IUser _user;

        public IUser User
        {
            get { return _user; }
            set { _user = value; }
        }

        public static MolecularIdentity ToIdentity(ClaimsIdentity identity)
        {


            MolecularIdentity myIdentity = new MolecularIdentity(identity, identity.Claims, identity.AuthenticationType, identity.NameClaimType, identity.RoleClaimType);

            //(myIdentity.User as IdentityUser).SetIdentiyFromClaims(identity);          
            return myIdentity;

        }

    }
}


