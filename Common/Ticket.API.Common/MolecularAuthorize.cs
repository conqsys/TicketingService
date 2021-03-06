﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.API.Common
{
   
    public class AuthorizeMolecular : AuthorizeAttribute
    {
       // public string Roles { get; set; }

            
        public AuthorizeMolecular(): base()
        {
            //this.Roles =  "LabUser,LabAdmin,SysAdmin";
        }        
    }

    public class AuthorizeLabAdmin : AuthorizeMolecular
    {
        public AuthorizeLabAdmin(): base()
        {
            this.Roles = "LabAdmin,SysAdmin";
        }
    }

    public class AuthorizeSystemAdmin : AuthorizeMolecular
    {
        public AuthorizeSystemAdmin() : base()
        {
            this.Roles = "SysAdmin";
        }
    }

}
