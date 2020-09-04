using MedicalSystem.Gateways.WebGateway.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.Gateways.WebGateway
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
                PatientApiUrl = "http://localhost:1234"
            };
            Assert.AreEqual("http://localhost:1234", patientOptions.PatientApiUrl);
        }
    }
}
