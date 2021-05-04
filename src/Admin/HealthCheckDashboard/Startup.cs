using HealthChecks.UI.Client;
using MedicalSystem.Admin.HealthCheckDashboard.ApiHealthChecks;
using MedicalSystem.Admin.HealthCheckDashboard.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MedicalSystem.Admin.HealthCheckDashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<DoctorOptions>(Configuration)
                .Configure<PatientOptions>(Configuration)
                .Configure<ConsultationOptions>(Configuration);

            services
                .AddHealthChecks()
                .AddCheck<DoctorApiHealthCheck>("doctor api check")
                .AddCheck<PatientApiHealthCheck>("patient api check")
                .AddCheck<ConsultationApiHealthCheck>("consultation api check");

            services
                .AddHealthChecksUI()
                .AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/doctor-hc", new HealthCheckOptions()
                {
                    Predicate = r => r.Name.Contains("doctor api check"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/patient-hc", new HealthCheckOptions()
                {
                    Predicate = r => r.Name.Contains("patient api check"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/consultation-hc", new HealthCheckOptions()
                {
                    Predicate = r => r.Name.Contains("consultation api check"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
