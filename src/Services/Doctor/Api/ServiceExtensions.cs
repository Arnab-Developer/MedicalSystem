using MedicalSystem.Services.Doctor.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MedicalSystem.Services.Doctor.Api
{
    internal static class ServiceExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string doctorDbConnectionString = configuration.GetConnectionString("DoctorDbConnectionString");
            services.AddDbContext<DoctorContext>(option => option.UseSqlServer(doctorDbConnectionString));
        }

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(
                    configuration.GetConnectionString("DoctorDbConnectionString"),
                    name: "DoctorDbCheck",
                    tags: new string[] { "DoctorDb" });

            return services;
        }
    }
}
