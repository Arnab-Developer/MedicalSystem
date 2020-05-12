namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <summary>
    /// Doctor domain model.
    /// </summary>
    internal class DoctorDomainModel
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
        /// Create new doctor object.
        /// </summary>
        /// <param name="id">Id of doctor.</param>
        /// <param name="firstName">First name of doctor.</param>
        /// <param name="lastName">Last name of doctor.</param>
        public DoctorDomainModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
