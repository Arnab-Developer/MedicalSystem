namespace MedicalSystem.FrontEnds.WebMvc.Options
{
    public class ConsultationOptions
    {
        public string ConsultationGatewayUrl { get; set; }

        public string ConsultationGatewayAddEditInitDataUrl { get; set; }

        public ConsultationOptions()
        {
            ConsultationGatewayUrl = string.Empty;
            ConsultationGatewayAddEditInitDataUrl = string.Empty;
        }
    }
}
