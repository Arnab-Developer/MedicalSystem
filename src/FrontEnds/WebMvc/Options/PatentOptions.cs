namespace MedicalSystem.FrontEnds.WebMvc.Options
{
    /// <include file='docs.xml' path='docs/members[@name="PatentOptions"]/patentOptions/*'/>
    public class PatentOptions
    {
        /// <include file='docs.xml' path='docs/members[@name="PatentOptions"]/patentGatewayUrl/*'/>
        public string PatentGatewayUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="PatentOptions"]/patentOptionsConstructor/*'/>
        public PatentOptions()
        {
            PatentGatewayUrl = string.Empty;
        }
    }
}
