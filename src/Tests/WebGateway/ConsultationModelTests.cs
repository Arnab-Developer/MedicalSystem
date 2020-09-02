using MedicalSystem.Gateways.WebGateway.Models;
using NUnit.Framework;
using System;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    internal class ConsultationModelTests
    {
        [Test]
        public void ConsultationModel_GivenValid_ReturnsValid()
        {
            var consultationModel = new ConsultationModel()
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
                Doctor = new DoctorModel()
                {
                    Id = 1,
                    FirstName = "doc1first",
                    LastName = "doc1last",
                },
                PatientId = 1,
                Patient = new PatientModel()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                }
            };
            Assert.AreEqual(1, consultationModel.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationModel.Date.Date);
            Assert.AreEqual("India", consultationModel.Country);
            Assert.AreEqual("WB", consultationModel.State);
            Assert.AreEqual("Kol", consultationModel.City);
            Assert.AreEqual("1234", consultationModel.PinCode);
            Assert.AreEqual("P1", consultationModel.Problem);
            Assert.AreEqual("M1", consultationModel.Medicine);
            Assert.AreEqual(1, consultationModel.DoctorId);
            Assert.AreEqual(1, consultationModel.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationModel.Doctor.FirstName);
            Assert.AreEqual("doc1last", consultationModel.Doctor.LastName);
            Assert.AreEqual(1, consultationModel.PatientId);
            Assert.AreEqual(1, consultationModel.Patient!.Id);
            Assert.AreEqual("pat1first", consultationModel.Patient.FirstName);
            Assert.AreEqual("pat1last", consultationModel.Patient.LastName);
        }
    }
}
