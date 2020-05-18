using MedicalSystem.Services.Patent.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Services.Patent.Data
{
    /// <include file='docs.xml' path='docs/members[@name="PatentContext"]/doctorContext/*'/>
    public class PatentContext : DbContext
    {
        /// <include file='docs.xml' path='docs/members[@name="PatentContext"]/doctorContextConstructor/*'/>
        public PatentContext(DbContextOptions<PatentContext> options)
            : base(options)
        {
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentContext"]/doctors/*'/>
        public DbSet<PatentModel>? Patents { get; set; }
    }
}
