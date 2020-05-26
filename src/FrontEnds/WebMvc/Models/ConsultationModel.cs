using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MedicalSystem.FrontEnds.WebMvc.Models
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/consultationModel/*'/>
    public class ConsultationModel
    {
        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/id/*'/>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/date/*'/>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/country/*'/>
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/state/*'/>
        [JsonPropertyName("state")]
        public string? State { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/city/*'/>
        [JsonPropertyName("city")]
        public string? City { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/pinCode/*'/>
        [JsonPropertyName("pinCode")]
        public string? PinCode { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/problem/*'/>
        [JsonPropertyName("problem")]
        public string? Problem { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/medicine/*'/>
        [JsonPropertyName("medicine")]
        public string? Medicine { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/doctorId/*'/>
        [JsonPropertyName("doctorId")]
        public int DoctorId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/doctor/*'/>
        [JsonPropertyName("doctor")]
        public DoctorModel? Doctor { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/patentId/*'/>
        [JsonPropertyName("patentId")]
        public int PatentId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/patent/*'/>
        [JsonPropertyName("patent")]
        public PatentModel? Patent { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/doctors/*'/>
        [JsonPropertyName("doctors")]
        public IEnumerable<DoctorModel>? Doctors { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/patents/*'/>
        [JsonPropertyName("patents")]
        public IEnumerable<PatentModel>? Patents { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/doctorSelectList/*'/>
        public IEnumerable<SelectListItem>? DoctorSelectList { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/patentSelectList/*'/>
        public IEnumerable<SelectListItem>? PatentSelectList { get; set; }
    }
}
