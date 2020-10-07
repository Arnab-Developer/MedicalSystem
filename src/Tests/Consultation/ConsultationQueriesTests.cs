using MedicalSystem.Services.Consultation;
using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.Queries;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class ConsultationQueriesTests
    {
        private IConsultationQueries? _consultationQueries;
        private ConsultationContext? _consultationContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ConsultationContext>()
                .UseInMemoryDatabase("ConsultationTestDb")
                .Options;
            _consultationContext = new ConsultationContext(options);
            _consultationQueries = new ConsultationQueries(_consultationContext);
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidViewModels()
        {
            AddDoctors();
            AddPatients();
            AddConsultations();

            List<ConsultationViewModel> consultationViewModels = _consultationQueries!.GetAll().ToList();

            Assert.AreEqual(2, consultationViewModels.Count);

            Assert.AreEqual(1, consultationViewModels[0].Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModels[0].Date.Date);
            Assert.AreEqual("India", consultationViewModels[0].Country);
            Assert.AreEqual("Maharashtra", consultationViewModels[0].State);
            Assert.AreEqual("Mumbai", consultationViewModels[0].City);
            Assert.AreEqual("123456", consultationViewModels[0].PinCode);
            Assert.AreEqual("Preg", consultationViewModels[0].Problem);
            Assert.AreEqual("Med1", consultationViewModels[0].Medicine);
            Assert.AreEqual(1, consultationViewModels[0].DoctorId);
            Assert.AreEqual(1, consultationViewModels[0].Doctor!.Id);
            Assert.AreEqual("doc1first", consultationViewModels[0].Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationViewModels[0].Doctor!.LastName);
            Assert.AreEqual(1, consultationViewModels[0].PatientId);
            Assert.AreEqual(1, consultationViewModels[0].Patient!.Id);
            Assert.AreEqual("pat1first", consultationViewModels[0].Patient!.FirstName);
            Assert.AreEqual("pat1last", consultationViewModels[0].Patient!.LastName);

            Assert.AreEqual(2, consultationViewModels[1].Id);
            Assert.AreEqual(DateTime.Now.AddDays(-1).Date, consultationViewModels[1].Date.Date);
            Assert.AreEqual("UK", consultationViewModels[1].Country);
            Assert.AreEqual("Bihar", consultationViewModels[1].State);
            Assert.AreEqual("Pune", consultationViewModels[1].City);
            Assert.AreEqual("987654", consultationViewModels[1].PinCode);
            Assert.AreEqual("Preg1", consultationViewModels[1].Problem);
            Assert.AreEqual("Med2", consultationViewModels[1].Medicine);
            Assert.AreEqual(2, consultationViewModels[1].DoctorId);
            Assert.AreEqual(2, consultationViewModels[1].Doctor!.Id);
            Assert.AreEqual("doc2first", consultationViewModels[1].Doctor!.FirstName);
            Assert.AreEqual("doc2last", consultationViewModels[1].Doctor!.LastName);
            Assert.AreEqual(2, consultationViewModels[1].PatientId);
            Assert.AreEqual(2, consultationViewModels[1].Patient!.Id);
            Assert.AreEqual("pat2first", consultationViewModels[1].Patient!.FirstName);
            Assert.AreEqual("pat2last", consultationViewModels[1].Patient!.LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyDomainModels()
        {
            AddDoctors();
            AddPatients();

            var consultationDomainModels = _consultationQueries!.GetAll().ToList();

            Assert.AreEqual(0, consultationDomainModels.Count);
        }

        [Test]
        public void GetById_GivenValidDbData_ReturnsValidViewModel()
        {
            AddDoctors();
            AddPatients();
            AddConsultation();

            ConsultationViewModel? consultationViewModel = _consultationQueries!.GetById(1);

            Assert.NotNull(consultationViewModel);

            Assert.AreEqual(1, consultationViewModel!.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModel.Date.Date);
            Assert.AreEqual("India", consultationViewModel.Country);
            Assert.AreEqual("Maharashtra", consultationViewModel.State);
            Assert.AreEqual("Mumbai", consultationViewModel.City);
            Assert.AreEqual("123456", consultationViewModel.PinCode);
            Assert.AreEqual("Preg", consultationViewModel.Problem);
            Assert.AreEqual("Med1", consultationViewModel.Medicine);
            Assert.AreEqual(1, consultationViewModel.DoctorId);
            Assert.AreEqual(1, consultationViewModel.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationViewModel.Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationViewModel.Doctor!.LastName);
            Assert.AreEqual(1, consultationViewModel.PatientId);
            Assert.AreEqual(1, consultationViewModel.Patient!.Id);
            Assert.AreEqual("pat1first", consultationViewModel.Patient!.FirstName);
            Assert.AreEqual("pat1last", consultationViewModel.Patient!.LastName);
        }

        [Test]
        public void GetById_GivenEmptyDbData_ReturnsNull()
        {
            AddDoctors();
            AddPatients();

            ConsultationViewModel? consultationViewModel = _consultationQueries!.GetById(1);

            Assert.Null(consultationViewModel);
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

        private void AddConsultation()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1);
            _consultationContext!.Consultations!.Add(consultationDomainModel);
            _consultationContext.SaveChanges();
        }
    }
}
