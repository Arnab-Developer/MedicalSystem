using MedicalSystem.Jobs.PatientSync.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Jobs.PatientSync.Data
{
    /// <include file='docs.xml' path='docs/members[@name="PatientContext"]/patientContext/*'/>
    internal class PatientContext : DbContext
    {
        /// <include file='docs.xml' path='docs/members[@name="PatientContext"]/patientContextConstructor/*'/>
        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientContext"]/patients/*'/>
        public DbSet<PatientModel>? Patients { get; set; }
    }
}
