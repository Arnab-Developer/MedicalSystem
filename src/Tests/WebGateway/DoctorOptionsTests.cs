using MedicalSystem.Gateways.WebGateway.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    internal class DoctorOptionsTests
    {
        [Test]
        public void DoctorOptions_GivenValid_ReturnsValid()
        {
            var doctorOptions = new DoctorOptions
            {
                DoctorApiUrl = "http://localhost:1234"
            };
            Assert.AreEqual("http://localhost:1234", doctorOptions.DoctorApiUrl);
        }
    }
}
