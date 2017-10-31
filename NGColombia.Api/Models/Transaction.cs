using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Models
{
    public class Transaction : Entity
    {
        public DateTime StartDate { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public string Ip { get; set; }
        public Customer Customer { get; set; }
        public double TotalValue { get; set; }
        public string Signature { get; set; }
        public bool Approved { get; set; }
        public bool Closed { get; set; }
        public string Status { get; set; }
        public bool IsTestTransaction { get; set; }

        public IEnumerable<TransactionTicketDetail> Details { get; set; }
        public IEnumerable<PaymentResponseLog> Responses { get; set; }
        public IEnumerable<PaymentConfirmationLog> Confirmations { get; set; }

        public double GetTotalValue()
        {
            if (Details == null || !Details.Any())
                return 0;

            return Details.Select(x => x.GetDetailSubTotal()).Sum();
        }

        public string GetTransactionDescription()
        {
            var events = string.Join(", ", Details.Select(detail => detail.TicketType.Name));
            return $"Tickets for NG-COLOMBIA: {events}";
        }

    }
}
