using System.Text.Json.Serialization;

namespace MedicalSystem.FrontEnds.WebMvc.Models
{
    public class ErrorModel
    {
        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        public ErrorModel()
        {
            Reason = string.Empty;
        }
    }
}
