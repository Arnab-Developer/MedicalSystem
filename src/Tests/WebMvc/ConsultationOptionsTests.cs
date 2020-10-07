using MedicalSystem.FrontEnds.WebMvc.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.FrontEnds.WebMvc
{
    internal class ConsultationOptionsTests
    {
        [Test]
        public void ConsultationModel_GivenValid_ReturnsValid()
        {
            var consultationOptions = new ConsultationOptions
            {
                ConsultationGatewayUrl = "http://localhost:1234",
                ConsultationGatewayAddEditInitDataUrl = "http://localhost:5678"
            };
            Assert.AreEqual("http://localhost:1234", consultationOptions.ConsultationGatewayUrl);
            Assert.AreEqual("http://localhost:5678", consultationOptions.ConsultationGatewayAddEditInitDataUrl);
        }
    }
}
