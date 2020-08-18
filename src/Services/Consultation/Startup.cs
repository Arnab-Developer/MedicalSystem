using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MedicalSystem.Tests.Services.Consultation")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace MedicalSystem.Services.Consultation
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
            services.AddGrpc();
            string consultationDbConnectionString = Configuration.GetConnectionString("ConsultationDbConnectionString");
            services.AddDbContext<ConsultationContext>(option => option.UseSqlServer(consultationDbConnectionString));
            services.AddTransient(typeof(IConsultationService), typeof(ConsultationService));
            services.AddTransient(typeof(IConsultationDal), typeof(ConsultationDal));
            services.AddTransient(typeof(IDoctorService), typeof(DoctorService));
            services.AddTransient(typeof(IDoctorDal), typeof(DoctorDal));
            services.AddTransient(typeof(IPatientService), typeof(PatientService));
            services.AddTransient(typeof(IPatientDal), typeof(PatientDal));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcServices.DoctorService>();
                endpoints.MapGrpcService<GrpcServices.PatientService>();
                endpoints.MapGrpcService<GrpcServices.ConsultationService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
