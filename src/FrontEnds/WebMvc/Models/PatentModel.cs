using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedicalSystem.FrontEnds.WebMvc.Models
{
    public class PatentModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("firstName")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public PatentModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
