using MedicalSystem.Jobs.DoctorSync.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Jobs.DoctorSync.Data
{
    internal class DoctorContext : DbContext
    {
        public DoctorContext(DbContextOptions<DoctorContext> options)
            : base(options)
        {
        }

        public DbSet<DoctorModel>? Doctors { get; set; }
    }
}