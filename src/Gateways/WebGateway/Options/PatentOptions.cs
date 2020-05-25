namespace MedicalSystem.Gateways.WebGateway.Options
{
    /// <include file='docs.xml' path='docs/members[@name="PatentOptions"]/patentOptions/*'/>
    public class PatentOptions
    {
        /// <include file='docs.xml' path='docs/members[@name="PatentOptions"]/patentApiUrl/*'/>
        public string PatentApiUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentOptions"]/patentOptionsConstructor/*'/>
        public PatentOptions()
        {
            PatentApiUrl = string.Empty;
        }
    }
}
