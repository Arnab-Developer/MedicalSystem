using MedicalSystem.Services.Consultation.Api.Commands;
using MedicalSystem.Services.Consultation.Api.ViewModels;
using NUnit.Framework;
using System;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class UpdateConsultationCommandTests
    {
        [Test]
        public void UpdateConsultationCommand_GivenValid_ReturnsValid()
        {
            UpdateConsultationCommand updateConsultationCommand = new UpdateConsultationCommand
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
            Assert.AreEqual(1, updateConsultationCommand.ConsultationViewModel.Id);
            Assert.AreEqual(DateTime.Now.Date, updateConsultationCommand.ConsultationViewModel.Date.Date);
            Assert.AreEqual("India", updateConsultationCommand.ConsultationViewModel.Country);
            Assert.AreEqual("WB", updateConsultationCommand.ConsultationViewModel.State);
            Assert.AreEqual("Kol", updateConsultationCommand.ConsultationViewModel.City);
            Assert.AreEqual("1234", updateConsultationCommand.ConsultationViewModel.PinCode);
            Assert.AreEqual("P1", updateConsultationCommand.ConsultationViewModel.Problem);
            Assert.AreEqual("M1", updateConsultationCommand.ConsultationViewModel.Medicine);
            Assert.AreEqual(1, updateConsultationCommand.ConsultationViewModel.DoctorId);
            Assert.AreEqual(1, updateConsultationCommand.ConsultationViewModel.Doctor!.Id);
            Assert.AreEqual("doc1first", updateConsultationCommand.ConsultationViewModel.Doctor.FirstName);
            Assert.AreEqual("doc1last", updateConsultationCommand.ConsultationViewModel.Doctor.LastName);
            Assert.AreEqual(1, updateConsultationCommand.ConsultationViewModel.PatientId);
            Assert.AreEqual(1, updateConsultationCommand.ConsultationViewModel.Patient!.Id);
            Assert.AreEqual("pat1first", updateConsultationCommand.ConsultationViewModel.Patient.FirstName);
            Assert.AreEqual("pat1last", updateConsultationCommand.ConsultationViewModel.Patient.LastName);
        }
    }
}
