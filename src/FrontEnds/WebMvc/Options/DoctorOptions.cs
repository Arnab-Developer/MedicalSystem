namespace MedicalSystem.FrontEnds.WebMvc.Options
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorOptions"]/doctorOptions/*'/>
    public class DoctorOptions
    {
        /// <include file='docs.xml' path='docs/members[@name="DoctorOptions"]/doctorGatewayUrl/*'/>
        public string DoctorGatewayUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorOptions"]/doctorOptionsConstructor/*'/>
        public DoctorOptions()
        {
            DoctorGatewayUrl = string.Empty;
        }
    }
}
