using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ticket.DataAccess;
using System.Security.Principal;
using Ticket.Base;

using System.ComponentModel.DataAnnotations;
using Ticket.API.Common;
using Ticket.BusinessLogic.Common;
using Ticket.DataAccess.Security;
using Ticket.DataAccess.Common;
using Ticket.BusinessLogic.Security;
using Dapper;
using Ticket.Base.Entities;
using Ticket.Base.Repositories;
using Ticket.Base.Services;


namespace Ticket.API.SecurityService
{
    [Route("api/[controller]")]
    public class RoleController : SecuredRepositoryController<IRoleRepository>
    {
        private IUserService _userService;
       
        
        public RoleController(IRoleRepository repository,
            IUserService userService
            ) 
            : base(repository)
        {
            this._userService = userService;
            
        }


        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody]Role newRole)
        {
            try
            {                
                var savedRole = await this.Repository.AddNew(newRole);
                return Ok(savedRole);
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
       
        [HttpGet("list")]
        public async Task<IPaginationQuery<IRole>> GetAllRoles([FromQuery]int? startPageNo, [FromQuery]int? endPageNo, [FromQuery]int? pageSize, [FromQuery]bool runCount = false)
        {
            return await this.Repository.FindAllRoles(startPageNo, endPageNo, pageSize, runCount);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody]Role updateRole)
        {
            try
            {
                var savedRole = await this.Repository.Update(updateRole);
                
                return Ok(savedRole);
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
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            try
            {
                await this._userService.DeleteRole(id);
                return Ok();
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
