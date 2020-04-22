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
    internal class PatentControllerTests
    {
        private PatentController? _patentController;
        private Mock<IPatentService>? _patentServiceMock;

        [SetUp]
        public void Setup()
        {
            _patentServiceMock = new Mock<IPatentService>();
            _patentController = new PatentController(_patentServiceMock.Object);
        }

        [Test]
        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
        {
            var patentViewModels = new List<PatentViewModel>()
            {
                new PatentViewModel()
                {
                    Id = 1,
                    FirstName = "doc1 f",
                    LastName = "doc1 l"
                },
                new PatentViewModel()
                {
                    Id = 2,
                    FirstName = "doc2 f",
                    LastName = "doc2 l"
                }
            };
            _patentServiceMock!.Setup(service => service.GetAll()).Returns(patentViewModels);

            var patentViewModelsFromController = _patentController!.GetAll().ToList();

            Assert.AreEqual(1, patentViewModelsFromController[0].Id);
            Assert.AreEqual("doc1 f", patentViewModelsFromController[0].FirstName);
            Assert.AreEqual("doc1 l", patentViewModelsFromController[0].LastName);

            Assert.AreEqual(2, patentViewModelsFromController[1].Id);
            Assert.AreEqual("doc2 f", patentViewModelsFromController[1].FirstName);
            Assert.AreEqual("doc2 l", patentViewModelsFromController[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
        {
            var patentViewModels = new List<PatentViewModel>();
            _patentServiceMock!.Setup(service => service.GetAll()).Returns(patentViewModels);
            var patentViewModelsFromController = _patentController!.GetAll();
            Assert.Zero(patentViewModelsFromController.Count());
        }

        [Test]
        public void GetAll_GivenException_ExpectException()
        {
            _patentServiceMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>(); ;
            Assert.Throws<NullReferenceException>(() => _patentController!.GetAll());
        }
    }
}
