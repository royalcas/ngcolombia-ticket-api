using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGColombia.Api.Filters
{
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var bodyStr = JsonConvert.SerializeObject(filterContext.ActionArguments);
                var telemetryClient = new TelemetryClient();
                var traceTelemetry = new TraceTelemetry
                {
                    Message = bodyStr,
                    SeverityLevel = SeverityLevel.Verbose
                };
                //Send a trace message for display in Diagnostic Search. 
                telemetryClient.TrackTrace(traceTelemetry);
            }
            catch { }
            

            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
            }
        }

        private static string GetRequestBody(HttpContext context)
        {
            var bodyStr = "";
            var req = context.Request;

            //Allows using several time the stream in ASP.Net Core.
            req.EnableRewind();

            //Important: keep stream opened to read when handling the request.
            using (var reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }

            // Rewind, so the core is not lost when it looks the body for the request.
            req.Body.Position = 0;
            return bodyStr;
        }

    }
}
