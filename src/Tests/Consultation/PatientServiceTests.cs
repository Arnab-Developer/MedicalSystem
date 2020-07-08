using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    /// <include file='docs.xml' path='docs/members[@name="PatientServiceTests"]/patientServiceTests/*'/>
    internal class PatientServiceTests
    {
        private IPatientService? _patientService;
        private Mock<IPatientDal>? _patientDalMock;

        /// <include file='docs.xml' path='docs/members[@name="PatientServiceTests"]/setup/*'/>
        [SetUp]
        public void Setup()
        {
            _patientDalMock = new Mock<IPatientDal>();
            _patientService = new PatientService(_patientDalMock.Object);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientServiceTests"]/getAll_GivenValidDomainModels_ReturnsValidViewModels/*'/>
        [Test]
        public void GetAll_GivenValidDomainModels_ReturnsValidViewModels()
        {
            var patientDomainModels = new List<PatientDomainModel>()
            {
                new PatientDomainModel(1, "doc1 f", "doc1 l"),
                new PatientDomainModel(2, "doc2 f", "doc2 l")
            };
            _patientDalMock!.Setup(dal => dal.GetAll()).Returns(patientDomainModels);

            List<PatientViewModel> patientViewModels = _patientService!.GetAll().ToList();

            Assert.AreEqual(1, patientViewModels[0].Id);
            Assert.AreEqual("doc1 f", patientViewModels[0].FirstName);
            Assert.AreEqual("doc1 l", patientViewModels[0].LastName);

            Assert.AreEqual(2, patientViewModels[1].Id);
            Assert.AreEqual("doc2 f", patientViewModels[1].FirstName);
            Assert.AreEqual("doc2 l", patientViewModels[1].LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientServiceTests"]/getAll_GivenEmptyDomainModels_ReturnsEmptyViewModels/*'/>
        [Test]
        public void GetAll_GivenEmptyDomainModels_ReturnsEmptyViewModels()
        {
            var patientDomainModels = new List<PatientDomainModel>();
            _patientDalMock!.Setup(dal => dal.GetAll()).Returns(patientDomainModels);
            List<PatientViewModel> patientViewModels = _patientService!.GetAll().ToList();
            Assert.Zero(patientViewModels.Count);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientServiceTests"]/getAll_GivenNullDomainModels_ExpectException/*'/>
        [Test]
        public void GetAll_GivenNullDomainModels_ExpectException()
        {
            _patientDalMock!.Setup(dal => dal.GetAll()).Returns<IEnumerable<PatientViewModel>>(null);
            Assert.Throws<NullReferenceException>(() => _patientService!.GetAll());
        }
    }
}
