using NGColombia.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Persistence
{
    public static class NGColombiaDbInitializer
    {
        public static void Initialize(NGColombiaDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.TicketTypes.Any())
            {
                return;   // DB has been seeded
            }

            SeedEvents(context);
            SeedTicketTypes(context);
            context.SaveChanges();
        }
        public static void SeedEvents(NGColombiaDbContext context)
        {
            var events = new List<Event>();
            events.Add(new Event()
            {
                Name = "NG-Colombia",
                ConfirmationDateLimit = new DateTime(2017, 11, 18),
                Description = "COLOMBIA'S FIRST ANGULAR CONFERENCE",
                Id = new Guid()
            });

            foreach (var currentEvent in events)
            {
                context.Events.Add(currentEvent);
            }
        }

        public static void SeedTicketTypes(NGColombiaDbContext context)
        {
            var ticketTypes = new List<TicketType>();
            ticketTypes.Add(new TicketType()
            {
                Name = Resource.TicketTypeInformation.MainConferenceName,
                Description = Resource.TicketTypeInformation.MainConferenceDescription,
                Code = Resource.TicketTypeCodes.MainConference,
                Value = 25
            });
            ticketTypes.Add(new TicketType()
            {
                Name = Resource.TicketTypeInformation.Workshop1Name,
                Code = Resource.TicketTypeCodes.Workshop1,
                Value = 20
            });
            ticketTypes.Add(new TicketType()
            {
                Name = Resource.TicketTypeInformation.Workshop2Name,
                Code = Resource.TicketTypeCodes.Workshop2,
                Value = 20
            });
            foreach (var ticket in ticketTypes)
            {
                context.TicketTypes.Add(ticket);
            }

        }
    }
}
