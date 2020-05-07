namespace MedicalSystem.Services.Patent.Models
{
    /// <summary>
    /// All the properties of patent.
    /// </summary>
    public class PatentModel
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
        /// Create a new object of patent.
        /// </summary>
        public PatentModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
