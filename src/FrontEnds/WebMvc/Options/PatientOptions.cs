namespace MedicalSystem.FrontEnds.WebMvc.Options
{
    public class PatientOptions
    {
        public string PatientGatewayUrl { get; set; }

        public PatientOptions()
        {
            PatientGatewayUrl = string.Empty;
        }
    }
}
