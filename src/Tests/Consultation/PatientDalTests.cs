using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    /// <include file='docs.xml' path='docs/members[@name="PatientDalTests"]/patientDalTests/*'/>
    internal class PatientDalTests
    {
        private IPatientDal? _patientDal;
        private ConsultationContext? _consultationContext;

        /// <include file='docs.xml' path='docs/members[@name="PatientDalTests"]/setup/*'/>
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ConsultationContext>()
                .UseInMemoryDatabase("ConsultationTestDb")
                .Options;
            _consultationContext = new ConsultationContext(options);
            _patientDal = new PatientDal(_consultationContext);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientDalTests"]/getAll_GivenValidDbData_ReturnsValidDomainModels/*'/>
        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidDomainModels()
        {
            AddPatients();
            var patientDomainModels = _patientDal!.GetAll().ToList();

            Assert.AreEqual(1, patientDomainModels[0].Id);
            Assert.AreEqual("doc1first", patientDomainModels[0].FirstName);
            Assert.AreEqual("doc1last", patientDomainModels[0].LastName);

            Assert.AreEqual(2, patientDomainModels[1].Id);
            Assert.AreEqual("doc2first", patientDomainModels[1].FirstName);
            Assert.AreEqual("doc2last", patientDomainModels[1].LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientDalTests"]/getAll_GivenEmptyDbData_ReturnsEmptyDomainModels/*'/>
        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyDomainModels()
        {
            var patientDomainModels = _patientDal!.GetAll().ToList();
            Assert.Zero(patientDomainModels.Count);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientDalTests"]/cleanup/*'/>
        [TearDown]
        public void Cleanup()
        {
            _consultationContext!.Patients!.RemoveRange(_consultationContext.Patients);
            _consultationContext.SaveChanges();
        }

        private void AddPatients()
        {
            var patients = new List<PatientDomainModel>()
            {
                new PatientDomainModel(1, "doc1first", "doc1last"),
                new PatientDomainModel(2, "doc2first", "doc2last")
            };
            _consultationContext!.Patients!.AddRange(patients);
            _consultationContext.SaveChanges();
        }
    }
}
