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
    public class TicketStatusController: SecuredRepositoryController<ITicketStatusRepository>
    {
        public TicketStatusController(ITicketStatusRepository repository) : base(repository)
        {

        }

        [HttpPost]
        public async Task<IActionResult> SaveTicketStatus([FromBody]TicketStatus ticketStatus)
        {
            try
            {
                var savedticketStatus = await this.Repository.AddNew(ticketStatus);
                return Ok(savedticketStatus);
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
        public async Task<IActionResult> UpdateRequestType([FromBody]TicketStatus ticketStatus)
        {
            try
            {
                var updatedTicketStatus = await this.Repository.Update(ticketStatus);

                return Ok(updatedTicketStatus);
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
        public async Task<IActionResult> DisableTicketStatus(long id)
        {
            try
            {

                var updatedTicketStatus = await this.Repository.ChangeActiveState(id, false, 1);  //this.Identity.User.Id
                return Ok(updatedTicketStatus);
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
        public async Task<IActionResult> EnableTicketStatus(long id)
        {
            try
            {
                var updatedTicketStatus = await this.Repository.ChangeActiveState(id, true, 1); //this.Identity.User.Id
                return Ok(updatedTicketStatus);
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
        public async Task<IEnumerable<ITicketStatus>> FindAll()
        {
            return await this.Repository.FindAllTicketStatus();
        }

    }
}
