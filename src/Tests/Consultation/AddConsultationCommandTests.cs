using MedicalSystem.Services.Consultation.Api.Commands;
using MedicalSystem.Services.Consultation.Api.ViewModels;
using NUnit.Framework;
using System;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class AddConsultationCommandTests
    {
        [Test]
        public void AddConsultationCommand_GivenValid_ReturnsValid()
        {
            var addConsultationCommand = new AddConsultationCommand
            {
                ConsultationViewModel = new ConsultationViewModel()
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
                }
            };
            Assert.AreEqual(1, addConsultationCommand.ConsultationViewModel.Id);
            Assert.AreEqual(DateTime.Now.Date, addConsultationCommand.ConsultationViewModel.Date.Date);
            Assert.AreEqual("India", addConsultationCommand.ConsultationViewModel.Country);
            Assert.AreEqual("WB", addConsultationCommand.ConsultationViewModel.State);
            Assert.AreEqual("Kol", addConsultationCommand.ConsultationViewModel.City);
            Assert.AreEqual("1234", addConsultationCommand.ConsultationViewModel.PinCode);
            Assert.AreEqual("P1", addConsultationCommand.ConsultationViewModel.Problem);
            Assert.AreEqual("M1", addConsultationCommand.ConsultationViewModel.Medicine);
            Assert.AreEqual(1, addConsultationCommand.ConsultationViewModel.DoctorId);
            Assert.AreEqual(1, addConsultationCommand.ConsultationViewModel.Doctor!.Id);
            Assert.AreEqual("doc1first", addConsultationCommand.ConsultationViewModel.Doctor.FirstName);
            Assert.AreEqual("doc1last", addConsultationCommand.ConsultationViewModel.Doctor.LastName);
            Assert.AreEqual(1, addConsultationCommand.ConsultationViewModel.PatientId);
            Assert.AreEqual(1, addConsultationCommand.ConsultationViewModel.Patient!.Id);
            Assert.AreEqual("pat1first", addConsultationCommand.ConsultationViewModel.Patient.FirstName);
            Assert.AreEqual("pat1last", addConsultationCommand.ConsultationViewModel.Patient.LastName);
        }
    }
}
