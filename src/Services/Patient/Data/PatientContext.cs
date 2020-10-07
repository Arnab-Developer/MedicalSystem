using MedicalSystem.Services.Patient.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Patient.Data
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
        }

        public DbSet<PatientModel>? Patients { get; set; }
    }
}
