#nullable disable

using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Consultation.Dals
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsultationDomainModel>()
                .OwnsOne(consultation => consultation.Place);
        }
    }
}
