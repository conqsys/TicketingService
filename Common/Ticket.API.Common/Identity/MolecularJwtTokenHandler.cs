using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.API.Common
{
    public class MolecularJwtTokenHandler: JwtSecurityTokenHandler
    {
        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            
            ClaimsPrincipal principal = base.ValidateToken(token, validationParameters, out validatedToken);
            var parsedToken=base.ReadJwtToken(token);
            MolecularIdentity identity = MolecularIdentity.ToIdentity(principal.Identity as ClaimsIdentity);            
            MolecularPrincipal molecularPrincipal = new MolecularPrincipal(identity);
            
            return molecularPrincipal;
            
        }
    }
}
