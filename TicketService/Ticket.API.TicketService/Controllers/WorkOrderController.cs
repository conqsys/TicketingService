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
    public class WorkOrderController : SecuredRepositoryController<IWorkOrderRepository>
    {
        public WorkOrderController(IWorkOrderRepository repository) : base(repository)
        {

        }


        [HttpPost]
        public async Task<IActionResult> SaveWorkOrder([FromBody]WorkOrder workOrder)
        {
            try
            {
                var savedWorkOrder = await this.Repository.AddNew(workOrder);
                return Ok(savedWorkOrder);
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
        public async Task<IActionResult> UpdateClientDetails([FromBody]WorkOrder workOrder)
        {
            try
            {
                var updateddWorkOrder = await this.Repository.Update(workOrder);

                return Ok(updateddWorkOrder);
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
        public async Task<IEnumerable<IWorkOrder>> FindAll()
        {
            return await this.Repository.FindAllWorkOrders();
        }

    }

}

