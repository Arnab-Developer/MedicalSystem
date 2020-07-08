using MedicalSystem.Services.Consultation.Controllers;
using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/patientControllerTests/*'/>
    internal class PatientControllerTests
    {
        private PatientController? _patientController;
        private Mock<IPatientService>? _patientServiceMock;

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/setup/*'/>
        [SetUp]
        public void Setup()
        {
            _patientServiceMock = new Mock<IPatientService>();
            _patientController = new PatientController(_patientServiceMock.Object);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getAll_GivenValidViewModels_ReturnsValidViewModels/*'/>
        [Test]
        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
        {
            var patientViewModels = new List<PatientViewModel>()
            {
                new PatientViewModel()
                {
                    Id = 1,
                    FirstName = "doc1 f",
                    LastName = "doc1 l"
                },
                new PatientViewModel()
                {
                    Id = 2,
                    FirstName = "doc2 f",
                    LastName = "doc2 l"
                }
            };
            _patientServiceMock!.Setup(service => service.GetAll()).Returns(patientViewModels);

            List<PatientViewModel> patientViewModelsFromController = _patientController!.GetAll().ToList();

            Assert.AreEqual(1, patientViewModelsFromController[0].Id);
            Assert.AreEqual("doc1 f", patientViewModelsFromController[0].FirstName);
            Assert.AreEqual("doc1 l", patientViewModelsFromController[0].LastName);

            Assert.AreEqual(2, patientViewModelsFromController[1].Id);
            Assert.AreEqual("doc2 f", patientViewModelsFromController[1].FirstName);
            Assert.AreEqual("doc2 l", patientViewModelsFromController[1].LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getAll_GivenEmptyViewModels_ReturnsEmptyViewModels/*'/>
        [Test]
        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
        {
            var patientViewModels = new List<PatientViewModel>();
            _patientServiceMock!.Setup(service => service.GetAll()).Returns(patientViewModels);
            IEnumerable<PatientViewModel> patientViewModelsFromController = _patientController!.GetAll();
            Assert.Zero(patientViewModelsFromController.Count());
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getAll_GivenException_ExpectException/*'/>
        [Test]
        public void GetAll_GivenException_ExpectException()
        {
            _patientServiceMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>(); ;
            Assert.Throws<NullReferenceException>(() => _patientController!.GetAll());
        }
    }
}
