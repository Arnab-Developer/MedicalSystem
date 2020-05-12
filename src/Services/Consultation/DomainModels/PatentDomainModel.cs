namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <summary>
    /// Patent model.
    /// </summary>
    internal class PatentDomainModel
    {
        /// <summary>
        /// Id of patent.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of patent.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of patent.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Create new patent object.
        /// </summary>
        /// <param name="id">Id of patent.</param>
        /// <param name="firstName">First name of patent.</param>
        /// <param name="lastName">Last name of patent.</param>
        public PatentDomainModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
