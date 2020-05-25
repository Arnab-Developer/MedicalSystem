namespace MedicalSystem.Gateways.WebGateway.Options
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationOptions/*'/>
    public class ConsultationOptions
    {
        /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationApiUrl/*'/>
        public string ConsultationApiUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationDoctorApiUrl/*'/>
        public string ConsultationDoctorApiUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationPatentApiUrl/*'/>
        public string ConsultationPatentApiUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationOptionsConstructor/*'/>
        public ConsultationOptions()
        {
            ConsultationApiUrl = string.Empty;
            ConsultationDoctorApiUrl = string.Empty;
            ConsultationPatentApiUrl = string.Empty;
        }
    }
}
