namespace MedicalSystem.Gateways.WebGateway.Options
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorOptions"]/doctorOptions/*'/>
    public class DoctorOptions
    {
        /// <include file='docs.xml' path='docs/members[@name="DoctorOptions"]/doctorApiUrl/*'/>
        public string DoctorApiUrl { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="DoctorOptions"]/doctorOptionsConstructor/*'/>
        public DoctorOptions()
        {
            DoctorApiUrl = string.Empty;
        }
    }
}
