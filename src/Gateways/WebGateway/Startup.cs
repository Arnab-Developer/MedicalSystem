using MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Patients;
using MedicalSystem.Gateways.WebGateway.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MedicalSystem.Gateways.WebGateway
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
            services.AddControllers();
            services.AddHttpClient();
            services.Configure<DoctorOptions>(Configuration);
            services.Configure<PatientOptions>(Configuration);
            services.Configure<ConsultationOptions>(Configuration);
            services.AddTransient(typeof(IDoctorGrpcClient), typeof(DoctorGrpcClient));
            services.AddTransient(typeof(IPatientGrpcClient), typeof(PatientGrpcClient));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
