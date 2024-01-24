using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using Presentation.ActionFilters;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Extensions;

namespace WebApi
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
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));

            services.AddControllers(config => 
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.CacheProfiles.Add("5mins", new CacheProfile() { Duration = 300 });
            }).AddXmlDataContractSerializerFormatters().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.ConfigureServiceManager();
            services.ConfigureLoggerService();
            services.AddAutoMapper(typeof(Program));
            services.ConfigureActionFilters();
            services.ConfigureResponseCaching();
            services.ConfigureHttpCacheHeaders();
            services.AddMemoryCache();
            services.ConfigureRateLimitingOptions();
            services.AddHttpContextAccessor();
            services.ConfigureIdentity();
            services.ConfigureJTW(Configuration);
           

            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILoggerService>();
            app.ConfigureExceptionHandler(logger);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            if(env.IsProduction())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseIpRateLimiting();

            app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
