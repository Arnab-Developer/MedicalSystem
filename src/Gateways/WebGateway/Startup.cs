using Grpc.Net.Client;
using HealthChecks.UI.Client;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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
            services.AddTransient<GrpcClients.Doctors.IDoctorGrpcClient>(options =>
            {
                GrpcChannel channel = GrpcChannel.ForAddress(Configuration["DoctorApiUrl"]);
                return new Protos.Doctors.Doctor.DoctorClient(channel);
            });
            services.AddTransient<GrpcClients.Patients.IPatientGrpcClient>(options =>
            {
                GrpcChannel channel = GrpcChannel.ForAddress(Configuration["PatientApiUrl"]);
                return new Protos.Patients.Patient.PatientClient(channel);
            });
            services.AddTransient<IDoctorGrpcClient>(options =>
            {
                GrpcChannel channel = GrpcChannel.ForAddress(Configuration["ConsultationApiUrl"]);
                return new Doctor.DoctorClient(channel);
            });
            services.AddTransient<IPatientGrpcClient>(options =>
            {
                GrpcChannel channel = GrpcChannel.ForAddress(Configuration["ConsultationApiUrl"]);
                return new Patient.PatientClient(channel);
            });
            services.AddTransient<IConsultationGrpcClient>(options =>
            {
                GrpcChannel channel = GrpcChannel.ForAddress(Configuration["ConsultationApiUrl"]);
                return new Consultation.ConsultationClient(channel);
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
