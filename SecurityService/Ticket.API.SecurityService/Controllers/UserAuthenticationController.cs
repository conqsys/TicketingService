using Microsoft.AspNetCore.Mvc;
using Ticket.API.Common;
using Ticket.Base;
using Ticket.Base.Repositories;
using Ticket.BusinessLogic.Common;
using Ticket.BusinessLogic.Security;
using Ticket.DataAccess;
using Ticket.DataAccess.Common;
using Ticket.DataAccess.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.API.SecurityService
{
    [Route("api/[controller]")]
    public class UserAuthenticationController : SecuredRepositoryController<IUserAuthenticationRepository>
    {
        public UserAuthenticationController(IUserAuthenticationRepository repository) : base(repository)
        {
        }


        [HttpPost("{userId}/{ipaddress}/{email}/{phone}")]
        public async Task<IActionResult> AddAuthentication(long userId, string ipAddress, string email, string phone)
        {
            try
            {
                var authecticateUser = await this.Repository.AddNew(userId, ipAddress, email, phone);
                return Ok(authecticateUser);
            }
            catch (MolecularException ex)
            {
                return StatusCode(400, ex.ValidationCodeResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("{userid}/{ipaddress}/{verificationCode}")]
        public async Task<IActionResult> VerifyAuthentication(long userId, string ipAddress, string verificationCode)
        {
            try
            {
                var authecticateUser = await this.Repository.VerifiyAuthentication(userId, ipAddress, verificationCode);
                return Ok(authecticateUser);
            }
            catch (MolecularException ex)
            {
                return StatusCode(400, ex.ValidationCodeResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
