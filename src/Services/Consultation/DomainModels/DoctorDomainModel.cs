namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorDomainModel"]/doctorDomainModel/*'/>
    public class DoctorDomainModel
    {
        /// <include file='docs.xml' path='docs/members[@name="DoctorDomainModel"]/id/*'/>
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorDomainModel"]/firstName/*'/>
        public string FirstName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorDomainModel"]/lastName/*'/>
        public string LastName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorDomainModel"]/doctorDomainModelConstructor/*'/>
        public DoctorDomainModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
