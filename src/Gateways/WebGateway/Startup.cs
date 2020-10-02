using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using MedicalSystem.Gateways.WebGateway.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace MedicalSystem.Gateways.WebGateway
{
    [ExcludeFromCodeCoverage]
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
            services.AddTransient(typeof(GrpcClients.Doctors.IDoctorGrpcClient), typeof(GrpcClients.Doctors.DoctorGrpcClient));
            services.AddTransient(typeof(GrpcClients.Patients.IPatientGrpcClient), typeof(GrpcClients.Patients.PatientGrpcClient));
            services.AddTransient(typeof(IDoctorGrpcClient), typeof(DoctorGrpcClient));
            services.AddTransient(typeof(IPatientGrpcClient), typeof(PatientGrpcClient));
            services.AddTransient(typeof(IConsultationGrpcClient), typeof(ConsultationGrpcClient));
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
