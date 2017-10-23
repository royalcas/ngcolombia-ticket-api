using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NGColombia.Api.Settings;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Service
{
    public class RecaptchaTokenValidator : IRecaptchaTokenValidator
    {
        private readonly IOptions<RecaptchaConfiguration> settings;

        public RecaptchaTokenValidator(IOptions<RecaptchaConfiguration> settings)
        { 
            this.settings = settings;
        }

        public async Task<bool> IsValidToken(string token)
        {
            if (!settings.Value.Validate)
            {
                return true;
            }

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();
            var client = new RestClient(settings.Value.EndpointUrl);

            var request = new RestRequest(string.Empty, Method.POST);
            request.AddParameter("secret", settings.Value.Secret);
            request.AddParameter("response", token);
            
            var handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            IRestResponse response = (RestResponse)(await taskCompletion.Task);
            var responseObject =  JsonConvert.DeserializeObject<Dto.Input.RecaptchaResponse>(response.Content);
            
            return responseObject.Success;
        }

    }
}
