using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Settings
{
    public class RecaptchaConfiguration
    {
        public string EndpointUrl { get; set; }
        public string Secret { get; set; }
        public string SiteKey { get; set; }
        public bool Validate { get; set; }
    }
}
