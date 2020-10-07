using MedicalSystem.FrontEnds.WebMvc.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.FrontEnds.WebMvc
{
    internal class PatientOptionsTests
    {
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
