using MedicalSystem.FrontEnds.WebMvc.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.FrontEnds.WebMvc
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationModelTests"]/consultationModelTests/*'/>
    internal class ConsultationOptionsTests
    {
        /// <include file='docs.xml' path='docs/members[@name="ConsultationModelTests"]/consultationModel_GivenValid_ReturnsValid/*'/>
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
