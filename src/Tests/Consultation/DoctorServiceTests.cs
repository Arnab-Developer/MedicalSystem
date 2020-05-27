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
    /// <include file='docs.xml' path='docs/members[@name="DoctorServiceTests"]/doctorServiceTests/*'/>
    internal class DoctorServiceTests
    {
        private DoctorService? _doctorService;
        private Mock<IDoctorDal>? _doctorDalMock;

        /// <include file='docs.xml' path='docs/members[@name="DoctorServiceTests"]/setup/*'/>
        [SetUp]
        public void Setup()
        {
            _doctorDalMock = new Mock<IDoctorDal>();
            _doctorService = new DoctorService(_doctorDalMock.Object);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorServiceTests"]/getAll_GivenValidDomainModels_ReturnsValidViewModels/*'/>
        [Test]
        public void GetAll_GivenValidDomainModels_ReturnsValidViewModels()
        {
            var doctorDomainModels = new List<DoctorDomainModel>()
            {
                new DoctorDomainModel(1, "doc1 f", "doc1 l"),
                new DoctorDomainModel(2, "doc2 f", "doc2 l")
            };
            _doctorDalMock!.Setup(dal => dal.GetAll()).Returns(doctorDomainModels);

            var doctorViewModels = _doctorService!.GetAll().ToList();

            Assert.AreEqual(1, doctorViewModels[0].Id);
            Assert.AreEqual("doc1 f", doctorViewModels[0].FirstName);
            Assert.AreEqual("doc1 l", doctorViewModels[0].LastName);

            Assert.AreEqual(2, doctorViewModels[1].Id);
            Assert.AreEqual("doc2 f", doctorViewModels[1].FirstName);
            Assert.AreEqual("doc2 l", doctorViewModels[1].LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorServiceTests"]/getAll_GivenEmptyDomainModels_ReturnsEmptyViewModels/*'/>
        [Test]
        public void GetAll_GivenEmptyDomainModels_ReturnsEmptyViewModels()
        {
            var doctorDomainModels = new List<DoctorDomainModel>();
            _doctorDalMock!.Setup(dal => dal.GetAll()).Returns(doctorDomainModels);
            var doctorViewModels = _doctorService!.GetAll();
            Assert.Zero(doctorViewModels.Count());
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorServiceTests"]/getAll_GivenNullDomainModels_ExpectException/*'/>
        [Test]
        public void GetAll_GivenNullDomainModels_ExpectException()
        {
            _doctorDalMock!.Setup(dal => dal.GetAll()).Returns<IEnumerable<DoctorViewModel>>(null);
            Assert.Throws<NullReferenceException>(() => _doctorService!.GetAll());
        }
    }
}
