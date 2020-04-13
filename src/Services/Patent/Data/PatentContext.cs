using MedicalSystem.Services.Patent.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Patent.Data
{
    public class PatentContext : DbContext
    {
        public PatentContext(DbContextOptions<PatentContext> options)
            : base(options)
        {
        }

        public DbSet<PatentModel>? Patents { get; set; }
    }
}
