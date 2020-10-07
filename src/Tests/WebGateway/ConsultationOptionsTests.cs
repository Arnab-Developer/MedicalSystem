using MedicalSystem.Gateways.WebGateway.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    internal class ConsultationOptionsTests
    {
        [Test]
        public void ConsultationModel_GivenValid_ReturnsValid()
        {
            var consultationOptions = new ConsultationOptions
            {
                ConsultationApiUrl = "http://localhost:1234"
            };
            Assert.AreEqual("http://localhost:1234", consultationOptions.ConsultationApiUrl);
        }
    }
}
