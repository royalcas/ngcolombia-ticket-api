using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Settings
{
    public class PayUSettings
    {
        public string GatewayUrl { get; set; }
        public string MerchantId { get; set; }
        public string AccountId { get; set; }
        public string ResposeUrl { get; set; }
        public string ConfirmationUrl { get; set; }
        public string ApiKey { get; set; }
        public string Test { get; set; }
        public string Currency { get; set; }
        public string AppLogin { get; set; }
        public string ApiUrl { get; set; }
    }
}
