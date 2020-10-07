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
    internal class PatientQueriesTests
    {
        private PatientQueries? _patientQueries;
        private ConsultationContext? _consultationContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ConsultationContext>()
                .UseInMemoryDatabase("ConsultationTestDb")
                .Options;
            _consultationContext = new ConsultationContext(options);
            _patientQueries = new PatientQueries(_consultationContext);
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidViewModels()
        {
            AddDoctors();
            AddPatients();
            AddConsultations();

            List<PatientViewModel> patientViewModels = _patientQueries!.GetAll().ToList();

            Assert.AreEqual(2, patientViewModels.Count);

            Assert.AreEqual(1, patientViewModels[0].Id);
            Assert.AreEqual("pat1first", patientViewModels[0].FirstName);
            Assert.AreEqual("pat1last", patientViewModels[0].LastName);

            Assert.AreEqual(2, patientViewModels[1].Id);
            Assert.AreEqual("pat2first", patientViewModels[1].FirstName);
            Assert.AreEqual("pat2last", patientViewModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyDomainModels()
        {
            List<PatientViewModel> patientDomainModels = _patientQueries!.GetAll().ToList();
            Assert.Zero(patientDomainModels.Count);
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
