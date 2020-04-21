namespace MedicalSystem.Services.Doctor.Models
{
    /// <summary>
    /// All the properties of docotor.
    /// </summary>
    public class DoctorModel
    {
        /// <summary>
        /// Id of doctor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of doctor.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of doctor.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Creates new object of doctor and initialize with default data.
        /// </summary>
        public DoctorModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
