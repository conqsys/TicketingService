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
    public class FacilityController : SecuredRepositoryController<IFacilityRepository>
    {
        public FacilityController(IFacilityRepository repository) : base(repository)
        {

        }


        [HttpPost]
        public async Task<IActionResult> SaveFacilityName([FromBody]Facility facility)
        {
            try
            {
                var savedFacilityName = await this.Repository.AddNew(facility);
                return Ok(savedFacilityName);
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
        public async Task<IActionResult> UpdateFacilityName([FromBody]Facility facility)
        {
            try
            {
                var updatedFacilityName = await this.Repository.Update(facility);

                return Ok(updatedFacilityName);
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
        public async Task<IActionResult> DisableFacilityName(long id)
        {
            try
            {

                var updatedFacilityName = await this.Repository.ChangeActiveState(id, false, 1);  //this.Identity.User.Id
                return Ok(updatedFacilityName);
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
        public async Task<IActionResult> EnableFacilityName(long id)
        {
            try
            {
                var updatedFacilityName = await this.Repository.ChangeActiveState(id, true, 1); //this.Identity.User.Id
                return Ok(updatedFacilityName);
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
        public async Task<IEnumerable<IFacility>> FindAll()
        {
            return await this.Repository.FindAllFacilities();
        }

    }

}

