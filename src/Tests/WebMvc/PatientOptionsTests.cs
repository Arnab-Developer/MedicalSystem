using MedicalSystem.FrontEnds.WebMvc.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.FrontEnds.WebMvc
{
    /// <include file='docs.xml' path='docs/members[@name="PatientOptionsTests"]/patientOptionsTests/*'/>
    internal class PatientOptionsTests
    {
        /// <include file='docs.xml' path='docs/members[@name="PatientOptionsTests"]/patientOptions_GivenValid_ReturnsValid/*'/>
        [Test]
        public void PatientOptions_GivenValid_ReturnsValid()
        {
            var patientOptions = new PatientOptions
            {
                PatientGatewayUrl = "http://localhost:1234"
            };
            Assert.AreEqual("http://localhost:1234", patientOptions.PatientGatewayUrl);
        }
    }
}
