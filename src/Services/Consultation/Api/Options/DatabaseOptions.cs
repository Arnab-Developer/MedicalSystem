namespace MedicalSystem.Services.Consultation.Api.Options
{
    public class DatabaseOptions
    {
        public const string CONNECTION_STRINGS = "ConnectionStrings";

        public string ConsultationDbConnectionString { get; set; }

        public DatabaseOptions()
        {
            ConsultationDbConnectionString = string.Empty;
        }
    }
}
