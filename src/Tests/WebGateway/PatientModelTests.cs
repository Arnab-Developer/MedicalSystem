using MedicalSystem.Gateways.WebGateway.Models;
using NUnit.Framework;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    internal class PatientModelTests
    {
        [Test]
        public void PatientModel_GivenValid_ReturnsValid()
        {
            var patientModel = new PatientModel
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            };
            Assert.AreEqual(1, patientModel.Id);
            Assert.AreEqual("f1", patientModel.FirstName);
            Assert.AreEqual("l1", patientModel.LastName);
        }
    }
}
