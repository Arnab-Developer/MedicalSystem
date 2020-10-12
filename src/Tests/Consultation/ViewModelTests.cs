using MedicalSystem.Services.Consultation.Api.ViewModels;
using NUnit.Framework;
using System;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class ViewModelTests
    {
        [Test]
        public void ConsultationViewModel_GivenValid_ReturnsValid()
        {
            var consultationViewModel = new ConsultationViewModel()
            {
                Id = 1,
                Date = DateTime.Now,
                Country = "India",
                State = "WB",
                City = "Kol",
                PinCode = "1234",
                Problem = "P1",
                Medicine = "M1",
                DoctorId = 1,
                Doctor = new DoctorViewModel()
                {
                    Id = 1,
                    FirstName = "doc1first",
                    LastName = "doc1last",
                },
                PatientId = 1,
                Patient = new PatientViewModel()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                }
            };
            Assert.AreEqual(1, consultationViewModel.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModel.Date.Date);
            Assert.AreEqual("India", consultationViewModel.Country);
            Assert.AreEqual("WB", consultationViewModel.State);
            Assert.AreEqual("Kol", consultationViewModel.City);
            Assert.AreEqual("1234", consultationViewModel.PinCode);
            Assert.AreEqual("P1", consultationViewModel.Problem);
            Assert.AreEqual("M1", consultationViewModel.Medicine);
            Assert.AreEqual(1, consultationViewModel.DoctorId);
            Assert.AreEqual(1, consultationViewModel.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationViewModel.Doctor.FirstName);
            Assert.AreEqual("doc1last", consultationViewModel.Doctor.LastName);
            Assert.AreEqual(1, consultationViewModel.PatientId);
            Assert.AreEqual(1, consultationViewModel.Patient!.Id);
            Assert.AreEqual("pat1first", consultationViewModel.Patient.FirstName);
            Assert.AreEqual("pat1last", consultationViewModel.Patient.LastName);
        }
    }
}
