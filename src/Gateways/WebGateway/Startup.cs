using HealthChecks.UI.Client;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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

            services.AddGrpcClient<Protos.Doctors.Doctor.DoctorClient>("DoctorService", o =>
            {
                o.Address = new Uri(Configuration["DoctorApiUrl"]);
            });
            services.AddTransient<GrpcClients.Doctors.IDoctorGrpcClient>(options =>
            {
                return options.GetRequiredService<Protos.Doctors.Doctor.DoctorClient>();
            });

            services.AddGrpcClient<Protos.Patients.Patient.PatientClient>("PatientService", o =>
            {
                o.Address = new Uri(Configuration["PatientApiUrl"]);
            });
            services.AddTransient<GrpcClients.Patients.IPatientGrpcClient>(options =>
            {
                return options.GetRequiredService<Protos.Patients.Patient.PatientClient>();
            });

            services.AddGrpcClient<Doctor.DoctorClient>("ConsultationDoctorService", o =>
            {
                o.Address = new Uri(Configuration["ConsultationApiUrl"]);
            });
            services.AddTransient<IDoctorGrpcClient>(options =>
            {
                return options.GetRequiredService<Doctor.DoctorClient>();
            });

            services.AddGrpcClient<Patient.PatientClient>("ConsultationPatientService", o =>
            {
                o.Address = new Uri(Configuration["ConsultationApiUrl"]);
            });
            services.AddTransient<IPatientGrpcClient>(options =>
            {
                return options.GetRequiredService<Patient.PatientClient>();
            });

            services.AddGrpcClient<Consultation.ConsultationClient>("ConsultationService", o =>
            {
                o.Address = new Uri(Configuration["ConsultationApiUrl"]);
            });
            services.AddTransient<IConsultationGrpcClient>(options =>
            {
                return options.GetRequiredService<Consultation.ConsultationClient>();
            });

            services.AddSwaggerGen();
            services.AddHealthChecks();
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
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllers();
            });
        }
    }
}
