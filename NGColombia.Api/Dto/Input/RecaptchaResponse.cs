using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Dto.Input
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("challenge_ts")]
        public string ChallengeTimestamp { get; set; }
        [JsonProperty("hostname")]
        public string Hostname { get; set; }
    }
}
