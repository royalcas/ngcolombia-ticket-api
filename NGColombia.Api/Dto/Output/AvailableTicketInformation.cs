using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Dto.Output
{
    public class AvailableTicketInformation
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int AvailableTickets { get; set; }
    }
}
