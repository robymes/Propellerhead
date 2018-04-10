using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetEscapades.AspNetCore.SecurityHeaders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Formatting.Json;
using StructureMap;
using System;
using System.IO.Compression;

namespace RobyMes.Propellerhead.Web
{
    public class Startup
    {
        protected Container container;        

        public IConfiguration Configuration
        {
            get;
        }

        public Startup(IConfiguration configuration)
        {
            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.None
                };
            };
            this.Configuration = configuration;
        }

        protected virtual void CreateLogger()
        {
            //Serilog logger configuration: async to console in JSON format
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(config => config.Console(new JsonFormatter()))
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddMvc();
            this.container = new Container(c =>
            {
                c.AddRegistry<ContainerRegistry>();
            });
            this.container.Populate(services);
            return this.container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            app.UseResponseCompression();
            var policyCollection = new HeaderPolicyCollection()
                .AddFrameOptionsSameOrigin() // prevent click-jacking
                .AddXssProtectionBlock() // prevent cross-site scripting (XSS)
                .AddContentTypeOptionsNoSniff(); // prevent drive-by-downloads
            app.UseSecurityHeaders(policyCollection);
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            loggerFactory
                .AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
