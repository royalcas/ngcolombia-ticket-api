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
    public class PayUConfirmation: IValidatableObject
    {
        [DataMember(Name = "merchant_id")]
        [JsonProperty("merchant_id")]
        public int merchant_id { get; set; }
        [StringLength(32)]
        [JsonProperty( "state_pol")]
        public string state_pol { get; set; }
        [DataMember(Name="response_code_col")]
        public string response_code_pol { get; set; }
        [JsonProperty( "response_sale")]
        public string reference_sale { get; set; }
        [JsonProperty( "response_pol")]
        public string reference_pol { get; set; }
        [JsonProperty( "value")]
        public double value { get; set; }
        public string currency { get; set; }
        public string sign { get; set; }
        [JsonProperty( "payment_method")]
        public string payment_method { get; set; }
        [JsonProperty( "payment_method_type")]
        public string payment_method_type { get; set; }
        [JsonProperty( "response_message_pol")]
        public string response_message_pol { get; set; }
        [JsonProperty( "transaction_id")]
        public string transaction_id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var paymentProvider = (IPaymentProvider)validationContext
                         .GetService(typeof(IPaymentProvider));
            var expectedSignature = paymentProvider.GetSignature(merchant_id.ToString(), reference_sale, value, currency, state_pol);

            if (!expectedSignature.Equals(sign, StringComparison.OrdinalIgnoreCase))
            {
                yield return ValidationResult.Success;
                //yield return new ValidationResult("This request is not verified by the payment provider");
            }           

        }
    }
}
