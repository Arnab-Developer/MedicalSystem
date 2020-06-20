using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MedicalSystem.Gateways.WebGateway.Models
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationModel"]/consultationModel/*'/>
    public class ConsultationModel
    {
        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/id/*'/>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/date/*'/>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/country/*'/>
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/state/*'/>
        [JsonPropertyName("state")]
        public string? State { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/city/*'/>
        [JsonPropertyName("city")]
        public string? City { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/pinCode/*'/>
        [JsonPropertyName("pinCode")]
        public string? PinCode { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/problem/*'/>
        [JsonPropertyName("problem")]
        public string? Problem { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/medicine/*'/>
        [JsonPropertyName("medicine")]
        public string? Medicine { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/doctorId/*'/>
        [JsonPropertyName("doctorId")]
        public int DoctorId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/doctor/*'/>
        [JsonPropertyName("doctor")]
        public DoctorModel? Doctor { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/patientId/*'/>
        [JsonPropertyName("patientId")]
        public int PatientId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/patient/*'/>
        [JsonPropertyName("patient")]
        public PatientModel? Patient { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/doctors/*'/>
        public IEnumerable<DoctorModel>? Doctors { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/patients/*'/>
        public IEnumerable<PatientModel>? Patients { get; set; }
    }
}
