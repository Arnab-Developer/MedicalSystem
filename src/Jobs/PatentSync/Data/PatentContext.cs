using MedicalSystem.Jobs.PatentSync.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Jobs.PatentSync.Data
{
    internal class PatentContext : DbContext
    {
        /// <summary>
        /// Creates a new DbContext object.
        /// </summary>
        /// <param name="options"></param>
        public PatentContext(DbContextOptions<PatentContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for patent.
        /// </summary>
        public DbSet<PatentModel>? Patents { get; set; }
    }
}
