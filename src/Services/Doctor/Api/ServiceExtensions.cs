using MedicalSystem.Services.Doctor.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalSystem.Services.Doctor.Api
{
    internal static class ServiceExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string doctorDbConnectionString = configuration.GetConnectionString("DoctorDbConnectionString");
            services.AddDbContext<DoctorContext>(option => option.UseSqlServer(doctorDbConnectionString));
        }
    }
}
