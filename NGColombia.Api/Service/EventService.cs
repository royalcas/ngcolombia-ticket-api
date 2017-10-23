namespace NGColombia.Api.Service
{
    using NGColombia.Api.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Dto.Output;
    using System.Collections;
    using Microsoft.EntityFrameworkCore;
    using NGColombia.Api.Models;

    public class EventService : IEventService
    {
        private NGColombiaDbContext context;

        public EventService(NGColombiaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AvailableTicketInformation>> GetAvailableTicketsSummary() {
            var ticketTypes = await context.TicketTypes
                                .Include(type => type.Details)
                                    .ThenInclude(detail => detail.Transaction)
                                    .ToListAsync();

            return MapToOutput(ticketTypes);
        }

        private IEnumerable<AvailableTicketInformation> MapToOutput(IEnumerable<TicketType> types)
        {
            return types.Select(type => new AvailableTicketInformation()
            {
                Code = type.Code,
                Name = type.Name,
                AvailableTickets = type.GetAvailableTicketsQuantity()
            });
        }
            
    }
}
