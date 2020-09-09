using MedicalSystem.FrontEnds.WebMvc.Models;
using NUnit.Framework;

namespace MedicalSystem.Tests.FrontEnds.WebMvc
{
    internal class DoctorModelTests
    {
        [Test]
        public void DoctorModel_GivenValid_ReturnsValid()
        {
            var doctorModel = new DoctorModel
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            };
            Assert.AreEqual(1, doctorModel.Id);
            Assert.AreEqual("f1", doctorModel.FirstName);
            Assert.AreEqual("l1", doctorModel.LastName);
        }
    }
}
