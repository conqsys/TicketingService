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
    public class ProcessController : SecuredRepositoryController<IProcessRepository>
    {
        public ProcessController(IProcessRepository repository) : base(repository)
        {

        }


        [HttpPost]
        public async Task<IActionResult> SaveProcessName([FromBody]Process process)
        {
            try
            {
                var savedProcessName = await this.Repository.AddNew(process);
                return Ok(savedProcessName);
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
        public async Task<IActionResult> UpdateProcessName([FromBody]Process process)
        {
            try
            {
                var updatedProcessName = await this.Repository.Update(process);

                return Ok(updatedProcessName);
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
        public async Task<IActionResult> DisableProcessName(long id)
        {
            try
            {

                var updatedProcessName = await this.Repository.ChangeActiveState(id, false, 1);  //this.Identity.User.Id
                return Ok(updatedProcessName);
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
        public async Task<IActionResult> EnableProcessName(long id)
        {
            try
            {
                var updatedProcessName = await this.Repository.ChangeActiveState(id, true, 1); //this.Identity.User.Id
                return Ok(updatedProcessName);
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
        public async Task<IEnumerable<IProcess>> FindAll()
        {
            return await this.Repository.FindAllProcess();
        }

    }

}

