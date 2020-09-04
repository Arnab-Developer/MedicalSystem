using MedicalSystem.Gateways.WebGateway.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorOptionsTests"]/doctorOptionsTests/*'/>
    internal class DoctorOptionsTests
    {
        /// <include file='docs.xml' path='docs/members[@name="DoctorOptionsTests"]/doctorOptions_GivenValid_ReturnsValid/*'/>
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
