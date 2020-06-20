using MedicalSystem.Services.Consultation.DomainModels;
using NUnit.Framework;
using System;

namespace MedicalSystem.Tests.Services.Consultation
{
    /// <include file='docs.xml' path='docs/members[@name="DomainModelTests"]/domainModelTests/*'/>
    internal class DomainModelTests
    {
        /// <include file='docs.xml' path='docs/members[@name="DomainModelTests"]/consultationDomainModel_GivenValid_ReturnsValid/*'/>
        [Test]
        public void ConsultationDomainModel_GivenValid_ReturnsValid()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };

            Assert.AreEqual(1, consultationDomainModel.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationDomainModel.Date.Date);
            Assert.AreEqual("India", consultationDomainModel.Place!.Country);
            Assert.AreEqual("Maharashtra", consultationDomainModel.Place!.State);
            Assert.AreEqual("Mumbai", consultationDomainModel.Place!.City);
            Assert.AreEqual("123456", consultationDomainModel.Place!.PinCode);
            Assert.AreEqual("Preg", consultationDomainModel.Problem);
            Assert.AreEqual("Med1", consultationDomainModel.Medicine);
            Assert.AreEqual(1, consultationDomainModel.DoctorId);
            Assert.AreEqual(1, consultationDomainModel.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationDomainModel.Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationDomainModel.Doctor!.LastName);
            Assert.AreEqual(1, consultationDomainModel.PatientId);
            Assert.AreEqual(1, consultationDomainModel.Patient!.Id);
            Assert.AreEqual("pat1first", consultationDomainModel.Patient!.FirstName);
            Assert.AreEqual("pat1last", consultationDomainModel.Patient!.LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="DomainModelTests"]/consultationDomainModel_GivenNullPlace_ExpectException/*'/>
        [Test]
        public void ConsultationDomainModel_GivenNullPlace_ExpectException()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };

            Assert.Throws<ArgumentException>(() => consultationDomainModel.Place = null);
        }

        /// <include file='docs.xml' path='docs/members[@name="DomainModelTests"]/consultationDomainModel_GivenNullProblem_ExpectException/*'/>
        [Test]
        public void ConsultationDomainModel_GivenNullProblem_ExpectException()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };

            Assert.Throws<ArgumentException>(() => consultationDomainModel.Problem = null);
        }

        /// <include file='docs.xml' path='docs/members[@name="DomainModelTests"]/consultationDomainModel_GivenBigLengthProblem_ExpectException/*'/>
        [Test]
        public void ConsultationDomainModel_GivenBigLengthProblem_ExpectException()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };

            Assert.Throws<ArgumentException>(() =>
                consultationDomainModel.Problem = "this is a very big text....");
        }

        /// <include file='docs.xml' path='docs/members[@name="DomainModelTests"]/consultationDomainModel_GivenNullMedicine_ExpectException/*'/>
        [Test]
        public void ConsultationDomainModel_GivenNullMedicine_ExpectException()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };

            Assert.Throws<ArgumentException>(() => consultationDomainModel.Medicine = null);
        }

        /// <include file='docs.xml' path='docs/members[@name="DomainModelTests"]/consultationDomainModel_GivenBigLengthMedicine_ExpectException/*'/>
        [Test]
        public void ConsultationDomainModel_GivenBigLengthMedicine_ExpectException()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };

            Assert.Throws<ArgumentException>(() =>
                consultationDomainModel.Medicine = "this is a very big text....");
        }
    }
}
