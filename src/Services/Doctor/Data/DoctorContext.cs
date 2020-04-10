using MedicalSystem.Services.Doctor.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Doctor.Data
{
    public class DoctorContext : DbContext
    {
        public DoctorContext(DbContextOptions<DoctorContext> options)
            : base(options)
        {
        }

        public DbSet<DoctorModel> Doctors { get; set; }
    }
}
