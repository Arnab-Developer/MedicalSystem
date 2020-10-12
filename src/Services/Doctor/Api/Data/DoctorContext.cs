using MedicalSystem.Services.Doctor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Doctor.Api.Data
{
    public class DoctorContext : DbContext
    {
        public DoctorContext(DbContextOptions<DoctorContext> options)
            : base(options)
        {
        }

        public DbSet<DoctorModel>? Doctors { get; set; }
    }
}
