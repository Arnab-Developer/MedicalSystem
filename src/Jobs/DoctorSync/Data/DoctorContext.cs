using MedicalSystem.Jobs.DoctorSync.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Jobs.DoctorSync.Data
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorContext"]/doctorContext/*'/>
    internal class DoctorContext : DbContext
    {
        /// <include file='docs.xml' path='docs/members[@name="DoctorContext"]/doctorContextConstructor/*'/>
        public DoctorContext(DbContextOptions<DoctorContext> options)
            : base(options)
        {
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorContext"]/doctors/*'/>
        public DbSet<DoctorModel>? Doctors { get; set; }
    }
}