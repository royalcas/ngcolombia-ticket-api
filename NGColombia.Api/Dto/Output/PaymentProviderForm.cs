using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Dto.Output
{
    public class PaymentProviderForm
    {
        public string ActionUrl { get; set; }
        public string MerchantId { get; set; }
        public string AccountId { get; set; }
        public string Description { get; set; }
        public string ReferenceCode { get; set; }
        public double Amount { get; set; }
        public double Tax { get; set; }
        public double TaxReturnBase { get; set; }
        public string Currency { get; set; }
        public string Signature { get; set; }
        public string Test { get; set; }
        public string BuyerFullName { get; set; }
        public string BuyerEmail { get; set; }
        public string ResponseUrl { get; set; }
        public string ConfirmationUrl { get; set; }
    }
}
