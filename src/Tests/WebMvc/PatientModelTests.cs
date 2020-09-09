using MedicalSystem.FrontEnds.WebMvc.Models;
using NUnit.Framework;

namespace MedicalSystem.Tests.FrontEnds.WebMvc
{
    /// <include file='docs.xml' path='docs/members[@name="PatientModelTests"]/patientModelTests/*'/>
    internal class PatientModelTests
    {
        /// <include file='docs.xml' path='docs/members[@name="PatientModelTests"]/patientModel_GivenValid_ReturnsValid/*'/>
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
