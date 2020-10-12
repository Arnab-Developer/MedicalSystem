using MedicalSystem.Services.Patient.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Patient.Api.Data
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
