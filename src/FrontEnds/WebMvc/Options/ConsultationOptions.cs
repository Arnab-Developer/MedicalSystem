namespace MedicalSystem.FrontEnds.WebMvc.Options
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationOptions/*'/>
    public class ConsultationOptions
    {
        /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationGatewayUrl/*'/>
        public string ConsultationGatewayUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationGatewayAddEditInitDataUrl/*'/>
        public string ConsultationGatewayAddEditInitDataUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationOptions"]/consultationOptionsConstructor/*'/>
        public ConsultationOptions()
        {
            ConsultationGatewayUrl = string.Empty;
            ConsultationGatewayAddEditInitDataUrl = string.Empty;
        }
    }
}
