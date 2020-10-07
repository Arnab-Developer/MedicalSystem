using MedicalSystem.FrontEnds.WebMvc.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.FrontEnds.WebMvc
{
    internal class DoctorOptionsTests
    {
        [Test]
        public void DoctorOptions_GivenValid_ReturnsValid()
        {
            var doctorOptions = new DoctorOptions
            {
                DoctorGatewayUrl = "http://localhost:1234"
            };
            Assert.AreEqual("http://localhost:1234", doctorOptions.DoctorGatewayUrl);
        }
    }
}
