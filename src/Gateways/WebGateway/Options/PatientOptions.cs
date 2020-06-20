namespace MedicalSystem.Gateways.WebGateway.Options
{
    /// <include file='docs.xml' path='docs/members[@name="PatientOptions"]/patientOptions/*'/>
    public class PatientOptions
    {
        /// <include file='docs.xml' path='docs/members[@name="PatientOptions"]/patientApiUrl/*'/>
        public string PatientApiUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatientOptions"]/patientOptionsConstructor/*'/>
        public PatientOptions()
        {
            PatientApiUrl = string.Empty;
        }
    }
}
