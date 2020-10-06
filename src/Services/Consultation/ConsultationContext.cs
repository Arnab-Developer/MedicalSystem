using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Consultation
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationContext"]/consultationContext/*'/>
    public class ConsultationContext : DbContext, IUnitOfWork
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

        void IUnitOfWork.SaveChanges()
        {
            SaveChanges();
        }
    }
}
