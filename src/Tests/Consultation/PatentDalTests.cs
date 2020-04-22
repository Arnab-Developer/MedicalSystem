using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class PatentDalTests
    {
        private PatentDal? _patentDal;
        private ConsultationContext? _consultationContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ConsultationContext>()
                .UseInMemoryDatabase("ConsultationTestDb")
                .Options;
            _consultationContext = new ConsultationContext(options);
            _patentDal = new PatentDal(_consultationContext);
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidDomainModels()
        {
            AddPatents();
            var patentDomainModels = _patentDal!.GetAll().ToList();

            Assert.AreEqual(1, patentDomainModels[0].Id);
            Assert.AreEqual("doc1first", patentDomainModels[0].FirstName);
            Assert.AreEqual("doc1last", patentDomainModels[0].LastName);

            Assert.AreEqual(2, patentDomainModels[1].Id);
            Assert.AreEqual("doc2first", patentDomainModels[1].FirstName);
            Assert.AreEqual("doc2last", patentDomainModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyDomainModels()
        {
            var patentDomainModels = _patentDal!.GetAll().ToList();
            Assert.Zero(patentDomainModels.Count);
        }

        [TearDown]
        public void Cleanup()
        {
            _consultationContext!.Patents!.RemoveRange(_consultationContext.Patents);
            _consultationContext.SaveChanges();
        }

        private void AddPatents()
        {
            var patents = new List<PatentDomainModel>()
            {
                new PatentDomainModel(1, "doc1first", "doc1last"),
                new PatentDomainModel(2, "doc2first", "doc2last")
            };
            _consultationContext!.Patents!.AddRange(patents);
            _consultationContext.SaveChanges();
        }
    }
}
