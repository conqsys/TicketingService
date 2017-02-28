using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ticket.Base;
using Ticket.Base.Repositories;
using Ticket.Base.Entities;

namespace Ticket.API.Common
{
    public interface ISecuredIdentityController
    {
        MolecularIdentity Identity { get; }

        MolecularPrincipal User { get; }
    }
    
    [AuthorizeMolecular]
    public abstract class SecuredRepositoryController<TRepo> : BaseRepositoryController<TRepo>, ISecuredIdentityController
         where TRepo : class, IDepRepository
    {
        public SecuredRepositoryController(TRepo repository) : base(repository) { }

        public MolecularIdentity Identity
        {
            get
            {
                return (MolecularIdentity)this.Request.HttpContext.User.Identity;
            }
        }

        public new MolecularPrincipal User
        {
            get
            {
                return this.Identity.Principal;
            }
        }
    }

    [AuthorizeMolecular]
    public abstract class SecuredServiceController : Controller, ISecuredIdentityController
    {

        public MolecularIdentity Identity
        {
            get
            {
                return (MolecularIdentity)this.Request.HttpContext.User.Identity;
            }
        }

        public new MolecularPrincipal User
        {
            get
            {
                return this.Identity.Principal;
            }
        }
    }
}
