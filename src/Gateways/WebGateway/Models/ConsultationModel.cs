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
        [JsonPropertyName("state")]

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/state/*'/>
        public string? State { get; set; }
        [JsonPropertyName("city")]

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/city/*'/>
        public string? City { get; set; }
        [JsonPropertyName("pinCode")]

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/pinCode/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/patentId/*'/>
        [JsonPropertyName("patentId")]
        public int PatentId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/patent/*'/>
        [JsonPropertyName("patent")]
        public PatentModel? Patent { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/doctors/*'/>
        public IEnumerable<DoctorModel>? Doctors { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/patents/*'/>
        public IEnumerable<PatentModel>? Patents { get; set; }
    }
}
