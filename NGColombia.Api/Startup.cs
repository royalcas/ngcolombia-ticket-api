using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using NGColombia.Api.Persistence;
using NGColombia.Api.Service;
using NGColombia.Api.Settings;

namespace NGColombia.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                }); 

            services.AddDbContext<NGColombiaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NGColombiaDatabase")));

            // Add application services.
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IPaymentProvider, PaymentProvider>();
            services.AddTransient<IRecaptchaTokenValidator, RecaptchaTokenValidator>();
            services.Configure<ApiConfiguration>(Configuration.GetSection("Api"));
            services.Configure<PayUSettings>(Configuration.GetSection("PayUConfiguration"));
            services.Configure<RecaptchaConfiguration>(Configuration.GetSection("ReCaptcha"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
