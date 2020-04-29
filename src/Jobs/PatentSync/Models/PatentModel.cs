namespace MedicalSystem.Jobs.PatentSync.Models
{
    internal class PatentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PatentModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
