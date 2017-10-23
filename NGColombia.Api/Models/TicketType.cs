using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Models
{
    public class TicketType
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public int TicketQuantity { get; set; }
        
        public Event @event { get; set; }

        public IEnumerable<TransactionTicketDetail> Details { get; set; }

        public int GetAvailableTicketsQuantity()
        {
            var soldTicketsQuantity = Details.Where(detail => detail.Transaction.Approved && !detail.Transaction.IsTestTransaction).Select( detail => detail.Quantity ).Sum();
            return Math.Max(0, TicketQuantity - soldTicketsQuantity);
        }
    }
}
