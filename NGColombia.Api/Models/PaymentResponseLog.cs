using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Models
{
    public class PaymentResponseLog: Entity
    {
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime ProcessDate { get; set; }
        public string LapTransationState { get; set; }
        public string TransactionState { get; set; }
        public string PolTransactionState { get; set; }
        public string LapResponseCode { get; set; }
        public string PolResponseCode { get; set; }
        public string RawData { get; set; }
    }
}
