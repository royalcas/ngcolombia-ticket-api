using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGColombia.Api.Service;

namespace NGColombia.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Event")]
    public class EventController : Controller
    {
        private readonly IEventService service;

        public EventController(IEventService service)
        {
            this.service = service;
        }

        [HttpGet, Route("availabletickets")]
        public async Task<ActionResult> TicketsAvailable()
        {
            return Ok(await service.GetAvailableTicketsSummary());
        }

        [HttpGet, Route("transactions")]
        public async Task<ActionResult> Transactions()
        {
            return Ok(await service.GetTransactions());
        }
        [HttpGet, Route("processPending")]
        public async Task<ActionResult> ProcessPending()
        {
            return Ok(await service.ConfirmPendingTransactions());
        }
    }


}