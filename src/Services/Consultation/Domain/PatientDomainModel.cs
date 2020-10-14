using MedicalSystem.Services.Consultation.Domain.SeedWork;

namespace MedicalSystem.Services.Consultation.Domain
{
    public class PatientDomainModel : Entity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PatientDomainModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
