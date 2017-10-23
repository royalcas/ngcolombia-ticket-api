using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Models
{
    public class Event: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ConfirmationDateLimit { get; set; }
        public IEnumerable<TicketType> TicketTypes { get; set; }
    }
}
