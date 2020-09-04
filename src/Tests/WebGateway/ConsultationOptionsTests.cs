using MedicalSystem.Gateways.WebGateway.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.Gateways.WebGateway
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
                ConsultationApiUrl = "http://localhost:1234"
            };
            Assert.AreEqual("http://localhost:1234", consultationOptions.ConsultationApiUrl);
        }
    }
}
