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
using Ticket.DataAccess.Common;
using Dapper;
using Ticket.Base.Entities;
using Ticket.Base.Repositories;
using Ticket.DataAccess.TicketService;

namespace Molecular.API.TicketService.Controllers
{
    [Route("api/[controller]")]
    public class RequestTypeController : SecuredRepositoryController<IRequestTypeRepository>
    {
        public RequestTypeController(IRequestTypeRepository repository) : base(repository)
        {

        }

        // GET api/values
        [HttpGet("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost]
        public async Task<IActionResult> SaveRequestType([FromBody]RequestType requestType)
        {
            try
            {
                var savedRequestType = await this.Repository.AddNew(requestType);
                return Ok(savedRequestType);
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
        public async Task<IActionResult> UpdateRequestType([FromBody]RequestType requestType)
        {
            try
            {
                var updatedRequestType = await this.Repository.Update(requestType);

                return Ok(updatedRequestType);
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

        [HttpPost("{id}/disable")]
        public async Task<IActionResult> DisableRequestType(long id)
        {
            try
            {

                var updatedRequestType = await this.Repository.ChangeActiveState(id, false, 1);  //this.Identity.User.Id
                return Ok(updatedRequestType);
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

        [HttpPost("{id}/enable")]
        public async Task<IActionResult> EnableRequestType(long id)
        {
            try
            {
                var updatedRequestType = await this.Repository.ChangeActiveState(id, true, 1); //this.Identity.User.Id
                return Ok(updatedRequestType);
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
        public async Task<IEnumerable<IRequestType>> FindAll()
        {
            return await this.Repository.FindAllRequestTypes();
        }

    }

}

