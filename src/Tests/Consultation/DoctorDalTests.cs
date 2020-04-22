using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class DoctorDalTests
    {
        private DoctorDal? _doctorDal;
        private ConsultationContext? _consultationContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ConsultationContext>()
                .UseInMemoryDatabase("ConsultationTestDb")
                .Options;
            _consultationContext = new ConsultationContext(options);
            _doctorDal = new DoctorDal(_consultationContext);
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidDomainModels()
        {
            AddDoctors();
            var doctorDomainModels = _doctorDal!.GetAll().ToList();

            Assert.AreEqual(1, doctorDomainModels[0].Id);
            Assert.AreEqual("doc1first", doctorDomainModels[0].FirstName);
            Assert.AreEqual("doc1last", doctorDomainModels[0].LastName);

            Assert.AreEqual(2, doctorDomainModels[1].Id);
            Assert.AreEqual("doc2first", doctorDomainModels[1].FirstName);
            Assert.AreEqual("doc2last", doctorDomainModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyDomainModels()
        {
            var doctorDomainModels = _doctorDal!.GetAll().ToList();
            Assert.Zero(doctorDomainModels.Count);
        }

        [TearDown]
        public void Cleanup()
        {
            _consultationContext!.Doctors!.RemoveRange(_consultationContext.Doctors);
            _consultationContext.SaveChanges();
        }

        private void AddDoctors()
        {
            var doctors = new List<DoctorDomainModel>()
            {
                new DoctorDomainModel(1, "doc1first", "doc1last"),
                new DoctorDomainModel(2, "doc2first", "doc2last")
            };
            _consultationContext!.Doctors!.AddRange(doctors);
            _consultationContext.SaveChanges();
        }
    }
}
