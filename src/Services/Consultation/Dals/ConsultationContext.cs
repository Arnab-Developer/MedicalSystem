using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationContext"]/consultationContext/*'/>
    internal class ConsultationContext : DbContext
    {
        /// <include file='docs.xml' path='docs/members[@name="ConsultationContext"]/consultationContextConstructor/*'/>
        public ConsultationContext(DbContextOptions<ConsultationContext> options)
            : base(options)
        {
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationContext"]/consultations/*'/>
        public DbSet<ConsultationDomainModel>? Consultations { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationContext"]/doctors/*'/>
        public DbSet<DoctorDomainModel>? Doctors { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationContext"]/patients/*'/>
        public DbSet<PatientDomainModel>? Patients { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationContext"]/onModelCreating/*'/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsultationDomainModel>()
                .OwnsOne(consultation => consultation.Place);
        }
    }
}
