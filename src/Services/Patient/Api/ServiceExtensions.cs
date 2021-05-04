using MedicalSystem.Services.Patient.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalSystem.Services.Patient.Api
{
    internal static class ServiceExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string patientDbConnectionString = configuration.GetConnectionString("PatientDbConnectionString");
            services.AddDbContext<PatientContext>(option => option.UseSqlServer(patientDbConnectionString));
        }
    }
}
