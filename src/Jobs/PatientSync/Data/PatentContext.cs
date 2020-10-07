using MedicalSystem.Jobs.PatientSync.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Jobs.PatientSync.Data
{
    internal class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
        }

        public DbSet<PatientModel>? Patients { get; set; }
    }
}
