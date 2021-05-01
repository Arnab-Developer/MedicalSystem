using MedicalSystem.Services.Consultation.Domain;
using MedicalSystem.Services.Consultation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class ConsultationRepositoryTests
    {
        private ConsultationContext? _consultationContext;
        private IConsultationRepository? _consultationRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ConsultationContext>()
                .UseInMemoryDatabase("ConsultationTestDb")
                .Options;
            _consultationContext = new ConsultationContext(options);

            var consultationContext = new ConsultationContext(options);
            _consultationRepository = new ConsultationRepository(consultationContext);
        }

        [Test]
        public void GetById_GivenValid_ReturnValid()
        {
            AddDoctors();
            AddPatients();
            AddConsultations();

            ConsultationDomainModel? consultationDomainModel = _consultationRepository!.GetById(1);

            Assert.NotNull(consultationDomainModel);
            Assert.AreEqual(1, consultationDomainModel!.Id);
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

        [Test]
        public void Add_CanInsert()
        {
            AddDoctors();
            AddPatients();
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India", "Maharashtra", "Mumbai", "123456",
                "Preg", "Med1", 1, 1);
            _consultationRepository!.Add(consultationDomainModel);

            Assert.AreEqual(EntityState.Added, ((DbContext)_consultationRepository.UnitOfWork).Entry(consultationDomainModel).State);
        }

        [Test]
        public void Delete_CanRemove()
        {
            AddDoctors();
            AddPatients();
            AddConsultations();

            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India", "Maharashtra", "Mumbai", "123456",
                "Preg", "Med1", 1, 1);
            _consultationRepository!.Delete(consultationDomainModel);

            Assert.AreEqual(EntityState.Deleted, ((DbContext)_consultationRepository.UnitOfWork).Entry(consultationDomainModel).State);
        }

        [TearDown]
        public void Cleanup()
        {
            _consultationContext!.Doctors!.RemoveRange(_consultationContext.Doctors);
            _consultationContext!.Patients!.RemoveRange(_consultationContext.Patients);
            _consultationContext!.Consultations!.RemoveRange(_consultationContext.Consultations);
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

        private void AddPatients()
        {
            var patients = new List<PatientDomainModel>()
            {
                new PatientDomainModel(1, "pat1first", "pat1last"),
                new PatientDomainModel(2, "pat2first", "pat2last"),
            };
            _consultationContext!.Patients!.AddRange(patients);
            _consultationContext.SaveChanges();
        }

        private void AddConsultations()
        {
            var consultationDomainModels = new List<ConsultationDomainModel>()
            {
                new ConsultationDomainModel(1, DateTime.Now, "India", "Maharashtra", "Mumbai", "123456",
                    "Preg", "Med1", 1, 1),
                new ConsultationDomainModel(2, DateTime.Now.AddDays(-1), "UK", "Bihar", "Pune", "987654",
                    "Preg1", "Med2", 2, 2)
            };
            _consultationContext!.Consultations!.AddRange(consultationDomainModels);
            _consultationContext.SaveChanges();
        }
    }
}
