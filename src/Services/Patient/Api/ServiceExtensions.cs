using MedicalSystem.Services.Patient.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MedicalSystem.Services.Patient.Api
{
    internal static class ServiceExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string patientDbConnectionString = configuration.GetConnectionString("PatientDbConnectionString");
            services.AddDbContext<PatientContext>(option => option.UseSqlServer(patientDbConnectionString));
        }

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(
                    configuration.GetConnectionString("PatientDbConnectionString"),
                    name: "PatientDbCheck",
                    tags: new string[] { "PatientDb" });

            return services;
        }
    }
}
