﻿using System.Text.Json.Serialization;

namespace MedicalSystem.Gateways.WebGateway.Models
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/doctorModel/*'/>
    public class DoctorModel
    {
        /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/id/*'/>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/firstName/*'/>
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorModel"]/lastName/*'/>
        [JsonPropertyName("lastName")]
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
