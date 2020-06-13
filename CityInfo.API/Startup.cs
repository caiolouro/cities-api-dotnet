using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                // JSON is a default output format. This code adds XML as well˝
                .AddMvcOptions(action =>
                {
                    action.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                });

            // By default, the JSON is serialized with camel case for field names. If you need the JSON field name to be exacly the same as the class, you can use the code below:
            //.AddJsonOptions(action =>
            //{
            //    if (action.SerializerSettings.ContractResolver != null)
            //    {
            //        var contractResolver = action.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        contractResolver.NamingStrategy = null;
            //    }
            //});

            // Register the service so it can be injected elsewhere. Transient, so for every request a new instance of it will be created

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This is called after the ConfigureServices method
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler();
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async (context) => await context.Response.WriteAsync("Hello Error World!"));
                });
            }

            app.UseStatusCodePages();

            app.UseMvc();
        }
    }
}
