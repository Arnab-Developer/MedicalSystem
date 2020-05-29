using MedicalSystem.Jobs.PatentSync.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Jobs.PatentSync.Data
{
    /// <include file='docs.xml' path='docs/members[@name="PatentContext"]/patentContext/*'/>
    internal class PatentContext : DbContext
    {
        /// <include file='docs.xml' path='docs/members[@name="PatentContext"]/patentContextConstructor/*'/>
        public PatentContext(DbContextOptions<PatentContext> options)
            : base(options)
        {
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentContext"]/patents/*'/>
        public DbSet<PatentModel>? Patents { get; set; }
    }
}
