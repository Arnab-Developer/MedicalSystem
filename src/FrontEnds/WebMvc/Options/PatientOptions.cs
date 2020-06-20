namespace MedicalSystem.FrontEnds.WebMvc.Options
{
    /// <include file='docs.xml' path='docs/members[@name="PatientOptions"]/patientOptions/*'/>
    public class PatientOptions
    {
        /// <include file='docs.xml' path='docs/members[@name="PatientOptions"]/patientGatewayUrl/*'/>
        public string PatientGatewayUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatientOptions"]/patientOptionsConstructor/*'/>
        public PatientOptions()
        {
            PatientGatewayUrl = string.Empty;
        }
    }
}
