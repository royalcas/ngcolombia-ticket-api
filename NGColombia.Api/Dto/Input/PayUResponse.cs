using Newtonsoft.Json;
using NGColombia.Api.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NGColombia.Api.Dto.Input
{
    public class PayUResponse: IValidatableObject
    {
        public int MerchantId { get; set; }
        public int TransactionState { get; set; }
        public double Risk { get; set; }
        public string PolResponseCode { get; set; }
        public string PolTransactionState { get; set; }
        public string ReferenceCode { get; set; }
        [DataMember(Name = "reference_pol")]
        public string ReferencePol { get; set; }
        public string Signature { get; set; }
        public string PolPaymentMethod { get; set; }
        public int PolPaymentMethodType { get; set; }
        public int InstallmentsNumber { get; set; }
        [DataMember(Name = "TX_VALUE")]
        [JsonProperty("TX_VALUE")]
        public double TotalValue { get; set; }
        [DataMember(Name = "TX_TAX")]
        [JsonProperty("TX_TAX")]
        public double Taxes { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime ProcessingDate { get; set; }
        public string Currency { get; set; }
        public string Cus { get; set; }
        [DataType(DataType.Text)]
        [StringLength(255)]
        public string PseBank { get; set; }

        public string Description { get; set; }
        public string LapResponseCode { get; set; }

        public string LapPaymentMethod { get; set; }
        public string LapPaymentMethodType { get; set; }
        public string LapTransactionState { get; set; }
        public string Message { get; set; }
        public string AuthorizationCode { get; set; }
        public string PseCycle { get; set; }
        public string PseReference1 { get; set; }
        public string PseReference2 { get; set; }
        public string PseReference3 { get; set; }
        public string TransactionId { get; set; }
        public string TransactionCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var paymentProvider = (IPaymentProvider)validationContext
                         .GetService(typeof(IPaymentProvider));
            var expectedSignature = paymentProvider.GetSignature(MerchantId.ToString(), ReferenceCode, TotalValue, Currency, TransactionState.ToString());

            if (!expectedSignature.Equals(Signature, StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("This request is not verified by the payment provider");
            }

        }
    }
}
