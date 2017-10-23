using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Models
{
    public class TransactionTicketDetail: Entity
    {
        public TicketType TicketType { get; set; }
        public Transaction Transaction { get; set; }
        public int Quantity { get; set; }

        public string TicketTypeCode { get; set; }

        public double GetDetailSubTotal()
        {
            return ((double)Quantity) * TicketType.Value;
        }
    }
}
