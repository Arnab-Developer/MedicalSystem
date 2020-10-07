namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <include file='docs.xml' path='docs/members[@name="PatientDomainModel"]/patientDomainModel/*'/>
    public class PatientDomainModel
    {
        /// <include file='docs.xml' path='docs/members[@name="PatientDomainModel"]/id/*'/>
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatientDomainModel"]/firstName/*'/>
        public string FirstName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatientDomainModel"]/lastName/*'/>
        public string LastName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatientDomainModel"]/patientDomainModelConstructor/*'/>
        public PatientDomainModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
