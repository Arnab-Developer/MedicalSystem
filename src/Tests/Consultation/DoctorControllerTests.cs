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
    /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/doctorControllerTests/*'/>
    internal class DoctorControllerTests
    {
        private DoctorController? _doctorController;
        private Mock<IDoctorService>? _doctorServiceMock;

        /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/setup/*'/>
        [SetUp]
        public void Setup()
        {
            _doctorServiceMock = new Mock<IDoctorService>();
            _doctorController = new DoctorController(_doctorServiceMock.Object);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/getAll_GivenValidViewModels_ReturnsValidViewModels/*'/>
        [Test]
        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
        {
            var doctorViewModels = new List<DoctorViewModel>()
            {
                new DoctorViewModel()
                {
                    Id = 1,
                    FirstName = "doc1 f",
                    LastName = "doc1 l"
                },
                new DoctorViewModel()
                {
                    Id = 2,
                    FirstName = "doc2 f",
                    LastName = "doc2 l"
                }
            };
            _doctorServiceMock!.Setup(service => service.GetAll()).Returns(doctorViewModels);

            List<DoctorViewModel> doctorViewModelsFromController = _doctorController!.GetAll().ToList();

            Assert.AreEqual(1, doctorViewModelsFromController[0].Id);
            Assert.AreEqual("doc1 f", doctorViewModelsFromController[0].FirstName);
            Assert.AreEqual("doc1 l", doctorViewModelsFromController[0].LastName);

            Assert.AreEqual(2, doctorViewModelsFromController[1].Id);
            Assert.AreEqual("doc2 f", doctorViewModelsFromController[1].FirstName);
            Assert.AreEqual("doc2 l", doctorViewModelsFromController[1].LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/getAll_GivenEmptyViewModels_ReturnsEmptyViewModels/*'/>
        [Test]
        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
        {
            var doctorViewModels = new List<DoctorViewModel>();
            _doctorServiceMock!.Setup(service => service.GetAll()).Returns(doctorViewModels);
            IEnumerable<DoctorViewModel> doctorViewModelsFromController = _doctorController!.GetAll();
            Assert.Zero(doctorViewModelsFromController.Count());
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/getAll_GivenException_ExpectException/*'/>
        [Test]
        public void GetAll_GivenException_ExpectException()
        {
            _doctorServiceMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>(); ;
            Assert.Throws<NullReferenceException>(() => _doctorController!.GetAll());
        }
    }
}
