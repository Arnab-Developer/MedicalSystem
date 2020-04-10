using MedicalSystem.Services.Consultation.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Consultation.Data
{
    public class ConsultationContext : DbContext
    {
        public ConsultationContext(DbContextOptions<ConsultationContext> options)
            : base(options)
        {
        }

        public DbSet<ConsultationModel> Consultations { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<PatentModel> Patents { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ConsultationModel>()
        //        .HasOne(c => c.Doctor)
        //        .WithOne()
        //        .IsRequired(false);
        //    //.OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<ConsultationModel>()
        //        .HasOne(c => c.Patent)
        //        .WithOne()
        //        .IsRequired(false);
        //        //.OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
