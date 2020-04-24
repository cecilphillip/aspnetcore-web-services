using System.Net.NetworkInformation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductsApiServiceDemo.Data;
using Prometheus;
using Steeltoe.Discovery.Client;

namespace ProductsApiServiceDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDiscoveryClient(Configuration);

            services.AddDbContext<DemoApiDbContext>(opts =>
            {
                opts.UseSqlite("Data Source=products.db");
            });

            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddHealthChecks()
                .AddDbContextCheck<DemoApiDbContext>()
                .AddCheck("AlwaysHealthy", () => new HealthCheckResult(HealthStatus.Healthy))
                // .AddAsyncCheck("Ping", async () =>
                // {
                //     using (var ping = new Ping())
                //     {
                //         var result = await ping.SendPingAsync("google.com", 3500);
                //         return new HealthCheckResult(
                //             result.Status == IPStatus.Success
                //                 ? HealthStatus.Healthy : HealthStatus.Unhealthy
                //         );
                //     }
                // })
                ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API Service", Version = "v1" });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

                // Only loopback proxies are allowed by default.
                // Clear that restriction because forwarders are enabled by explicit 
                // configuration.
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseDiscoveryClient();

            app.UseRouting();
            app.UseHttpMetrics();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics("/metrics");
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}