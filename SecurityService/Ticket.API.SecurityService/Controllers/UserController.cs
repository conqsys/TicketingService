using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ticket.DataAccess;
using System.Security.Principal;
using Ticket.Base;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Ticket.API.Common;
using Ticket.BusinessLogic.Common;
using Ticket.DataAccess.Security;
using Ticket.DataAccess.Common;
using Ticket.Base.Entities;
using Ticket.BusinessLogic.Security;
using Dapper;
using Ticket.Base.Repositories;
using Ticket.Base.Services;


namespace Ticket.API.SecurityService
{
    [Route("api/[controller]")]
    public class UserController : SecuredRepositoryController<IUserRepository>
    {
        private IUserService _userService;
        public UserController(IUserRepository repository
           ) 
            : base(repository)
        {   
        }

        [HttpGet("me")]
        public async Task<IUser> GetIdenity()
        {
            /* this is MolecularIdentity now*/
            return this.Identity.User;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]User newUser)
        {
            try
            {
                var savedUser = await this.Repository.AddNew(newUser);
                return Ok(savedUser);
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

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]User updateUser)
        {
            try
            {
                var updatedUser = await this.Repository.Update(updateUser);
                return Ok(updatedUser);
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
        public async Task<IPaginationQuery<IUser>> GetAllUsers([FromQuery]int? startPageNo, [FromQuery]int? endPageNo, [FromQuery]int? pageSize, [FromQuery]bool runCount = false)
        {
            return await this.Repository.FindAll(startPageNo, endPageNo, pageSize, runCount);
        }


        [HttpGet("{roleId}/list")]
        public async Task<IEnumerable<IUser>> GetUsersByRoleId(long roleId)
        {
            return await this.Repository.FindAllByRoleId(roleId);
        }

        [HttpPost("{id}/disable")]
        public async Task<IActionResult> DisableUser(long id)
        {
            var updatedUser =  await this.Repository.ChangeActiveState(id, false, 1); //this.Identity.User.Id
            return Ok(updatedUser);
        }
        
        [HttpPost("{id}/enable")]
        public async Task<IActionResult> EnableUser(long id)
        {
            try
            {
                var updatedUser =  await this.Repository.ChangeActiveState(id, true, 1); //this.Identity.User.Id
                return Ok(updatedUser);
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

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await this._userService.LogOutUser(this.Identity.User.Id);
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

        [HttpPost("{id}/changepassword/{password}")]
        public async Task<IActionResult> ChangePassword(long id, string password)
        {
            try
            {
                await this._userService.ChangePassword(id, password);
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

        [HttpGet("getUserById/{id}")]
        public async Task<object> FindUserById(long id)
        {
            return await this.Repository.FindById(id);
        }
    }
}
