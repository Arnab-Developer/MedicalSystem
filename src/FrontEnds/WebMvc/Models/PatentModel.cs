using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedicalSystem.FrontEnds.WebMvc.Models
{
    /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/patentModel/*'/>
    public class PatentModel
    {
        /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/id/*'/>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/firstName/*'/>
        [JsonPropertyName("firstName")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/lastName/*'/>
        [JsonPropertyName("lastName")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentModel"]/patentModelConstructor/*'/>
        public PatentModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
