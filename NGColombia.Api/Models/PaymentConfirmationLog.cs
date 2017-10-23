using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Models
{
    public class PaymentConfirmationLog: Entity
    {
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime ProcessDate { get; set; }
        public string ReferencePayU { get; set; }
        public string StatePol { get; set; }
        public string ResponseMessagePol { get; set; }
        public string ResponseCodePol { get; set; }
        public string RawData { get; set; }
    }
}
