using Microsoft.IdentityModel.Tokens;
using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ticket.API.Common
{
    public class MolecularJwtToken: JwtSecurityToken
    {
        public MolecularJwtToken(MolecularJwtPayLoad payload) :base(new JwtHeader(),payload)
        {

        }

        public MolecularJwtToken(IUser user, string issuer, string audience, IEnumerable<Claim> claims, DateTime? notBefore, DateTime? expires, SigningCredentials signingCredentials) : base(new JwtHeader(signingCredentials), new MolecularJwtPayLoad(user,issuer, audience, claims, notBefore, expires))
        {

        }
    }

    public class MolecularJwtPayLoad : JwtPayload
    {
        public MolecularJwtPayLoad(IUser user, string issuer, string audience, IEnumerable<Claim> claims, DateTime? notBefore, DateTime? expires) :base(issuer,audience,claims,notBefore,expires)
        {
            this._user = user;
        }
        private IUser _user;

        public IUser User
        {
            get { return _user; }
            private set { _user = value; }
        }


    }
}
