namespace MedicalSystem.Gateways.WebGateway.Options
{
    public class ConsultationOptions
    {
        public string ConsultationApiUrl { get; set; }
        public string ConsultationDoctorApiUrl { get; set; }
        public string ConsultationPatentApiUrl { get; set; }

        public ConsultationOptions()
        {
            ConsultationApiUrl = string.Empty;
            ConsultationDoctorApiUrl = string.Empty;
            ConsultationPatentApiUrl = string.Empty;
        }
    }
}
