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

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/patientId/*'/>
        [JsonPropertyName("patientId")]
        public int PatientId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/patient/*'/>
        [JsonPropertyName("patient")]
        public PatientModel? Patient { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/doctors/*'/>
        [JsonPropertyName("doctors")]
        public IEnumerable<DoctorModel>? Doctors { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/patients/*'/>
        [JsonPropertyName("patients")]
        public IEnumerable<PatientModel>? Patients { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/doctorSelectList/*'/>
        public IEnumerable<SelectListItem>? DoctorSelectList { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/patientSelectList/*'/>
        public IEnumerable<SelectListItem>? PatientSelectList { get; set; }
    }
}
