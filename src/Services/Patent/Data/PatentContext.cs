using MedicalSystem.Services.Patent.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Patent.Data
{
    /// <summary>
    /// Data context of patent data.
    /// </summary>
    public class PatentContext : DbContext
    {
        /// <summary>
        /// Create a new object of patent context.
        /// </summary>
        /// <param name="options"></param>
        public PatentContext(DbContextOptions<PatentContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for patents.
        /// </summary>
        public DbSet<PatentModel>? Patents { get; set; }
    }
}
