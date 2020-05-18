namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <include file='docs.xml' path='docs/members[@name="PatentDomainModel"]/patentDomainModel/*'/>
    internal class PatentDomainModel
    {
        /// <include file='docs.xml' path='docs/members[@name="PatentDomainModel"]/id/*'/>
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentDomainModel"]/firstName/*'/>
        public string FirstName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentDomainModel"]/lastName/*'/>
        public string LastName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentDomainModel"]/patentDomainModelConstructor/*'/>
        public PatentDomainModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
