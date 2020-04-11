#nullable disable

using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Consultation.Data
{
    public class ConsultationContext : DbContext
    {
        public ConsultationContext(DbContextOptions<ConsultationContext> options)
            : base(options)
        {
        }

        public DbSet<ConsultationDomainModel> Consultations { get; set; }
        public DbSet<DoctorDomainModel> Doctors { get; set; }
        public DbSet<PatentDomainModel> Patents { get; set; }
    }
}
