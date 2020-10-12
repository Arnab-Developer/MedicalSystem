using MedicalSystem.Services.Consultation.Domain;
using MedicalSystem.Services.Consultation.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Consultation.Infrastructure
{
    public class ConsultationContext : DbContext, IUnitOfWork
    {
        public ConsultationContext(DbContextOptions<ConsultationContext> options)
            : base(options)
        {
        }

        public DbSet<ConsultationDomainModel>? Consultations { get; set; }

        public DbSet<DoctorDomainModel>? Doctors { get; set; }

        public DbSet<PatientDomainModel>? Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsultationDomainModel>()
                .OwnsOne(consultation => consultation.Place);
        }

        void IUnitOfWork.SaveChanges()
        {
            SaveChanges();
        }
    }
}
