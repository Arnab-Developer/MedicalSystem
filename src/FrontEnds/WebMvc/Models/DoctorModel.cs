using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MedicalSystem.FrontEnds.WebMvc.Models
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/doctorModel/*'/>
    public class DoctorModel
    {
        /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/id/*'/>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/firstName/*'/>
        [JsonPropertyName("firstName")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/lastName/*'/>
        [JsonPropertyName("lastName")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/doctorModelConstructor/*'/>
        public DoctorModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
