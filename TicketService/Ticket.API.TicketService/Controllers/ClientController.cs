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
    public class ClientController : SecuredRepositoryController<IClientRepository>
    {
        public ClientController(IClientRepository repository) : base(repository)
        {

        }


        [HttpPost]
        public async Task<IActionResult> SaveClientDetails([FromBody]Client client)
        {
            try
            {
                var savedClientDetails = await this.Repository.AddNew(client);
                return Ok(savedClientDetails);
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
        public async Task<IActionResult> UpdateClientDetails([FromBody]Client client)
        {
            try
            {
                var updatedClientDetails = await this.Repository.Update(client);

                return Ok(updatedClientDetails);
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
        public async Task<IActionResult> DisableClient(long id)
        {
            try
            {

                var updatedClientDetails = await this.Repository.ChangeActiveState(id, false, 1);  //this.Identity.User.Id
                return Ok(updatedClientDetails);
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
        public async Task<IActionResult> EnableClient(long id)
        {
            try
            {
                var updatedClientDetails = await this.Repository.ChangeActiveState(id, true, 1); //this.Identity.User.Id
                return Ok(updatedClientDetails);
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
        public async Task<IEnumerable<IClient>> FindAll()
        {
            return await this.Repository.FindAllClients();
        }

        [HttpGet("getClientById/{id}")]
        public async Task<object> FindClientById(long id) {
            return await this.Repository.FindById(id);
        }

    }

}

