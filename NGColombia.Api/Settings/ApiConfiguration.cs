using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Settings
{
    public class ApiConfiguration
    {
        public string SharedKey { get; set; }
        public string RecaptchaSecret { get; set; }
    }
}
