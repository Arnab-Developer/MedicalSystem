using MedicalSystem.Services.Patent.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Patent.Data
{
    /// <include file='docs.xml' path='docs/members[@name="PatentContext"]/patentContext/*'/>
    public class PatentContext : DbContext
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
