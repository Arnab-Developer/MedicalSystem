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
    internal class PatentServiceTests
    {
        private PatentService? _patentService;
        private Mock<IPatentDal>? _patentDalMock;

        [SetUp]
        public void Setup()
        {
            _patentDalMock = new Mock<IPatentDal>();
            _patentService = new PatentService(_patentDalMock.Object);
        }

        [Test]
        public void GetAll_GivenValidDomainModels_ReturnsValidViewModels()
        {
            var patentDomainModels = new List<PatentDomainModel>()
            {
                new PatentDomainModel(1, "doc1 f", "doc1 l"),
                new PatentDomainModel(2, "doc2 f", "doc2 l")
            };
            _patentDalMock!.Setup(dal => dal.GetAll()).Returns(patentDomainModels);

            var patentViewModels = _patentService!.GetAll().ToList();

            Assert.AreEqual(1, patentViewModels[0].Id);
            Assert.AreEqual("doc1 f", patentViewModels[0].FirstName);
            Assert.AreEqual("doc1 l", patentViewModels[0].LastName);

            Assert.AreEqual(2, patentViewModels[1].Id);
            Assert.AreEqual("doc2 f", patentViewModels[1].FirstName);
            Assert.AreEqual("doc2 l", patentViewModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDomainModels_ReturnsEmptyViewModels()
        {
            var patentDomainModels = new List<PatentDomainModel>();
            _patentDalMock!.Setup(dal => dal.GetAll()).Returns(patentDomainModels);
            var patentViewModels = _patentService!.GetAll().ToList();
            Assert.Zero(patentViewModels.Count);
        }

        [Test]
        public void GetAll_GivenNullDomainModels_ExpectException()
        {
            _patentDalMock!.Setup(dal => dal.GetAll()).Returns<IEnumerable<PatentViewModel>>(null);
            Assert.Throws<NullReferenceException>(() => _patentService!.GetAll());
        }
    }
}
