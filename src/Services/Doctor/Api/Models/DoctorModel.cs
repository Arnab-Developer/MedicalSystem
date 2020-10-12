namespace MedicalSystem.Services.Doctor.Api.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DoctorModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
