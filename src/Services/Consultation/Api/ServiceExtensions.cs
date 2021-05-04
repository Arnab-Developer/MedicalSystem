using MedicalSystem.Services.Consultation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalSystem.Services.Consultation.Api
{
    internal static class ServiceExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            string consultationDbConnectionString = Configuration.GetConnectionString("ConsultationDbConnectionString");
            services.AddDbContext<ConsultationContext>(option => option.UseSqlServer(consultationDbConnectionString));
        }
    }
}
