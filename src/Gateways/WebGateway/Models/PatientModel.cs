using System.Text.Json.Serialization;

namespace MedicalSystem.Gateways.WebGateway.Models
{
    /// <include file='docs.xml' path='docs/members[@name="PatientModel"]/patientModel/*'/>
    public class PatientModel
    {
        /// <include file='docs.xml' path='docs/members[@name="PatientModel"]/id/*'/>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatientModel"]/firstName/*'/>
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatientModel"]/lastName/*'/>
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatientModel"]/patientModelConstructor/*'/>
        public PatientModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
