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
    internal class DoctorQueriesTests
    {
        private DoctorQueries? _doctorQueries;
        private ConsultationContext? _consultationContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ConsultationContext>()
                .UseInMemoryDatabase("ConsultationTestDb")
                .Options;
            _consultationContext = new ConsultationContext(options);
            _doctorQueries = new DoctorQueries(_consultationContext);
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidViewModels()
        {
            AddDoctors();
            AddPatients();
            AddConsultations();

            List<DoctorViewModel> doctorViewModels = _doctorQueries!.GetAll().ToList();

            Assert.AreEqual(2, doctorViewModels.Count);

            Assert.AreEqual(1, doctorViewModels[0].Id);
            Assert.AreEqual("doc1first", doctorViewModels[0].FirstName);
            Assert.AreEqual("doc1last", doctorViewModels[0].LastName);

            Assert.AreEqual(2, doctorViewModels[1].Id);
            Assert.AreEqual("doc2first", doctorViewModels[1].FirstName);
            Assert.AreEqual("doc2last", doctorViewModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyDomainModels()
        {
            List<DoctorViewModel> doctorDomainModels = _doctorQueries!.GetAll().ToList();
            Assert.Zero(doctorDomainModels.Count);
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
