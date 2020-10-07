using MedicalSystem.Gateways.WebGateway.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    internal class PatientOptionsTests
    {
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
