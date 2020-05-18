namespace MedicalSystem.Services.Patent.Models
{
    /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/patentModel/*'/>
    public class PatentModel
    {
        /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/id/*'/>
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/firstName/*'/>
        public string FirstName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/lastName/*'/>
        public string LastName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/patentModelConstructor/*'/>
        public PatentModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
