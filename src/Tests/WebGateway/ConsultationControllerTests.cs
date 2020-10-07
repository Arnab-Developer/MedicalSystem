using Google.Protobuf.WellKnownTypes;
using MedicalSystem.Gateways.WebGateway.Controllers;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    internal class ConsultationControllerTests
    {
        private Mock<IDoctorGrpcClient>? _doctorGrpcClientMock;
        private Mock<IPatientGrpcClient>? _patientGrpcClientMock;
        private Mock<IConsultationGrpcClient>? _consultationGrpcClientMock;
        private ConsultationController? _consultationController;

        [SetUp]
        public void Setup()
        {
            _doctorGrpcClientMock = new Mock<IDoctorGrpcClient>();
            _patientGrpcClientMock = new Mock<IPatientGrpcClient>();
            _consultationGrpcClientMock = new Mock<IConsultationGrpcClient>();
            _consultationController = new ConsultationController(
                _doctorGrpcClientMock.Object,
                _patientGrpcClientMock.Object,
                _consultationGrpcClientMock.Object);
        }

        [Test]
        public void GetAll_GivenValidModelsMessage_ReturnsValidModels()
        {
            var consultationModelsMessage = new ConsultationModelsMessage();
            consultationModelsMessage.Consultations.Add(new ConsultationModelMessage
            {
                Id = 1,
                Date = DateTime.UtcNow.ToTimestamp(),
                Country = "India",
                State = "WB",
                City = "Kol",
                PinCode = "1234",
                Problem = "P1",
                Medicine = "M1",
                DoctorId = 1,
                Doctor = new DoctorModelMessage()
                {
                    Id = 1,
                    FirstName = "doc1first",
                    LastName = "doc1last",
                },
                PatientId = 1,
                Patient = new PatientModelMessage()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                }
            });

            _consultationGrpcClientMock!
                .Setup(m => m.GetAllAsync(It.IsAny<EmptyMessage>()))
                .Returns(Task.FromResult(consultationModelsMessage));

            IList<ConsultationModel> consultationModels
                = ((IEnumerable<ConsultationModel>)((OkObjectResult)_consultationController!.GetAll().Result.Result).Value).ToList();

            Assert.AreEqual(1, consultationModels[0].Id);
            Assert.AreEqual(DateTime.UtcNow.Date, consultationModels[0].Date.Date);
            Assert.AreEqual("India", consultationModels[0].Country);
            Assert.AreEqual("WB", consultationModels[0].State);
            Assert.AreEqual("Kol", consultationModels[0].City);
            Assert.AreEqual("1234", consultationModels[0].PinCode);
            Assert.AreEqual("P1", consultationModels[0].Problem);
            Assert.AreEqual("M1", consultationModels[0].Medicine);
            Assert.AreEqual(1, consultationModels[0].DoctorId);
            Assert.AreEqual(1, consultationModels[0].Doctor!.Id);
            Assert.AreEqual("doc1first", consultationModels[0].Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationModels[0].Doctor!.LastName);
            Assert.AreEqual(1, consultationModels[0].PatientId);
            Assert.AreEqual(1, consultationModels[0].Patient!.Id);
            Assert.AreEqual("pat1first", consultationModels[0].Patient!.FirstName);
            Assert.AreEqual("pat1last", consultationModels[0].Patient!.LastName);
        }

        [Test]
        public void GetAll_GivenEmptyModelsMessage_ReturnsErrorModels()
        {
            _consultationGrpcClientMock!
                .Setup(m => m.GetAllAsync(It.IsAny<EmptyMessage>()))
                .Returns(Task.FromResult(It.IsAny<ConsultationModelsMessage>()));

            ErrorModel error
                = (ErrorModel)((NotFoundObjectResult)_consultationController!.GetAll().Result.Result).Value;

            Assert.AreEqual("No consultation record found.", error.Reason);
        }

        [Test]
        public void GetById_GivenValidModelMessage_ReturnsValidModel()
        {
            var consultationModelMessage = new ConsultationModelMessage
            {
                Id = 1,
                Date = DateTime.UtcNow.ToTimestamp(),
                Country = "India",
                State = "WB",
                City = "Kol",
                PinCode = "1234",
                Problem = "P1",
                Medicine = "M1",
                DoctorId = 1,
                Doctor = new DoctorModelMessage()
                {
                    Id = 1,
                    FirstName = "doc1first",
                    LastName = "doc1last",
                },
                PatientId = 1,
                Patient = new PatientModelMessage()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                }
            };

            _consultationGrpcClientMock!
                .Setup(m => m.GetByIdAsync(It.IsAny<IdMessage>()))
                .Returns(Task.FromResult(consultationModelMessage));

            ConsultationModel consultationModel
                = (ConsultationModel)((OkObjectResult)_consultationController!.GetById(It.IsAny<int>()).Result.Result).Value;

            Assert.AreEqual(1, consultationModel.Id);
            Assert.AreEqual(DateTime.UtcNow.Date, consultationModel.Date.Date);
            Assert.AreEqual("India", consultationModel.Country);
            Assert.AreEqual("WB", consultationModel.State);
            Assert.AreEqual("Kol", consultationModel.City);
            Assert.AreEqual("1234", consultationModel.PinCode);
            Assert.AreEqual("P1", consultationModel.Problem);
            Assert.AreEqual("M1", consultationModel.Medicine);
            Assert.AreEqual(1, consultationModel.DoctorId);
            Assert.AreEqual(1, consultationModel.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationModel.Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationModel.Doctor!.LastName);
            Assert.AreEqual(1, consultationModel.PatientId);
            Assert.AreEqual(1, consultationModel.Patient!.Id);
            Assert.AreEqual("pat1first", consultationModel.Patient!.FirstName);
            Assert.AreEqual("pat1last", consultationModel.Patient!.LastName);
        }

        [Test]
        public void GetById_GivenEmptyModelsMessage_ReturnsErrorModels()
        {
            _consultationGrpcClientMock!
                .Setup(m => m.GetByIdAsync(It.IsAny<IdMessage>()))
                .Returns(Task.FromResult(It.IsAny<ConsultationModelMessage>()));

            ErrorModel error
                = (ErrorModel)((NotFoundObjectResult)_consultationController!.GetById(It.IsAny<int>()).Result.Result).Value;

            Assert.AreEqual("No consultation record found.", error.Reason);
        }

        [Test]
        public void Add_CanCallGrpcServiceAdd()
        {
            _consultationGrpcClientMock!
                .Setup(m => m.AddAsync(It.IsAny<ConsultationModelMessage>()))
                .Verifiable();
            IActionResult actionResult = _consultationController!.Add(
                new ConsultationModel
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
                }).Result;
            _consultationGrpcClientMock.Verify();
        }

        [Test]
        public void Update_CanCallGrpcServiceUpdate()
        {
            _consultationGrpcClientMock!
                .Setup(m => m.UpdateAsync(It.IsAny<UpdateMessage>()))
                .Verifiable();
            IActionResult actionResult = _consultationController!.Update(It.IsAny<int>(),
                new ConsultationModel
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
                }).Result;
            _consultationGrpcClientMock.Verify();
        }

        [Test]
        public void Delete_CanCallGrpcServiceDelete()
        {
            _consultationGrpcClientMock!
                .Setup(m => m.DeleteAsync(It.IsAny<IdMessage>()))
                .Verifiable();
            IActionResult actionResult = _consultationController!.Delete(It.IsAny<int>()).Result;
            _consultationGrpcClientMock.Verify();
        }
    }
}
