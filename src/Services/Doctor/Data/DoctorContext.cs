using MedicalSystem.Services.Doctor.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Doctor.Data
{
    /// <summary>
    /// Data context of doctor.
    /// </summary>
    public class DoctorContext : DbContext
    {
        /// <summary>
        /// Creates a new DbContext object.
        /// </summary>
        /// <param name="options"></param>
        public DoctorContext(DbContextOptions<DoctorContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for doctor.
        /// </summary>
        public DbSet<DoctorModel>? Doctors { get; set; }
    }
}
