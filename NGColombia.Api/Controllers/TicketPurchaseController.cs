using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGColombia.Api.Dto.Input;
using NGColombia.Api.Service;
using NGColombia.Api.Filters;
using System.Net;
using NGColombia.Api.Dto.Output;
using NGColombia.Api.Service.Exceptions;
using Microsoft.ApplicationInsights;

namespace NGColombia.Api.Controllers
{
    [Produces("application/json", "application/x-www-form-urlencoded")]
    [Route("api/purchase")]
    public class TicketPurchaseController : Controller
    {
        private readonly ITicketService service;
        private readonly TelemetryClient appInsights;

        public TicketPurchaseController(ITicketService service)
        {
            this.service = service;
            this.appInsights = new TelemetryClient();
        }

        [ValidateModel]
        public async Task<ObjectResult> Post([FromBody]TransactionInputModel model)
        {
            try
            {
                var result = await service.SaveInitialTransaction(model);
                return Ok(result);
            }
            catch(CheckoutUnavailableException ex)
            {
                var transactionResult = new TransactionResult()
                {
                    Success = false,
                    Message = ex.Message
                };

                return BadRequest(transactionResult);
            }

        }
        [ValidateModel]
        [HttpPost, Route("confirmation")]
        public async Task<ObjectResult> Confirmation(PayUConfirmation model)
        {
            try
            {
                var result = await service.Confirm(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                appInsights.TrackException(ex);
                throw;
            }
        }
        
        [HttpGet, Route("confirmation")]
        public async Task<ObjectResult> ConfirmationPage()
        {
            return Ok(true);
        }

        [ValidateModel]
        [HttpPost, Route("response")]
        public async Task<ObjectResult> ReceiveResponse([FromBody]PayUResponse model)
        {
            var result = await service.ProcessResponse(model);
            return Ok(result);
        }

        [ValidateModel]
        [HttpGet, Route("search")]
        public async Task<ObjectResult> ReceiveResponse([FromQuery] CustomerTransactionsQuery query)
        {
            var result = await service.FindCustomerTickets(query.query);
            return Ok(result);
        }

    }
}