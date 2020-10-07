using MedicalSystem.Gateways.WebGateway.Controllers;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Patients;
using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Protos.Patients;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    internal class PatientControllerTests
    {
        private Mock<IPatientGrpcClient>? _patientGrpcClientMock;
        private PatientController? _patientController;

        [SetUp]
        public void Setup()
        {
            _patientGrpcClientMock = new Mock<IPatientGrpcClient>();
            _patientController = new PatientController(_patientGrpcClientMock.Object);
        }

        [Test]
        public void GetAll_GivenValidModelsMessage_ReturnsValidModels()
        {
            var patientModelsMessage = new PatientModelsMessage();
            patientModelsMessage.Patients.Add(new PatientModelMessage
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            });
            _patientGrpcClientMock!
                .Setup(m => m.GetAllAsync(It.IsAny<EmptyMessage>()))
                .Returns(Task.FromResult(patientModelsMessage));

            IList<PatientModel> patientModels
                = ((IEnumerable<PatientModel>)((OkObjectResult)_patientController!.GetAll().Result.Result).Value).ToList();

            Assert.AreEqual(patientModelsMessage.Patients[0].Id, patientModels[0].Id);
            Assert.AreEqual(patientModelsMessage.Patients[0].FirstName, patientModels[0].FirstName);
            Assert.AreEqual(patientModelsMessage.Patients[0].LastName, patientModels[0].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyModelsMessage_ReturnsErrorModels()
        {
            var patientModelsMessage = new PatientModelsMessage();
            _patientGrpcClientMock!
                .Setup(m => m.GetAllAsync(It.IsAny<EmptyMessage>()))
                .Returns(Task.FromResult(patientModelsMessage));

            ErrorModel error
                = (ErrorModel)((NotFoundObjectResult)_patientController!.GetAll().Result.Result).Value;

            Assert.AreEqual("No patient record found.", error.Reason);
        }

        [Test]
        public void GetById_GivenValidModelMessage_ReturnsValidModel()
        {
            var patientModelMessage = new PatientModelMessage
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            };
            _patientGrpcClientMock!
                .Setup(m => m.GetByIdAsync(It.IsAny<IdMessage>()))
                .Returns(Task.FromResult(patientModelMessage));

            PatientModel patientModel
                = (PatientModel)((OkObjectResult)_patientController!.GetById(It.IsAny<int>()).Result.Result).Value;

            Assert.AreEqual(patientModelMessage.Id, patientModel.Id);
            Assert.AreEqual(patientModelMessage.FirstName, patientModel.FirstName);
            Assert.AreEqual(patientModelMessage.LastName, patientModel.LastName);
        }

        [Test]
        public void GetById_GivenEmptyModelsMessage_ReturnsErrorModels()
        {
            _patientGrpcClientMock!
                .Setup(m => m.GetByIdAsync(It.IsAny<IdMessage>()))
                .Returns(Task.FromResult(It.IsAny<PatientModelMessage>()));

            ErrorModel error
                = (ErrorModel)((NotFoundObjectResult)_patientController!.GetById(It.IsAny<int>()).Result.Result).Value;

            Assert.AreEqual("No patient record found.", error.Reason);
        }

        [Test]
        public void Add_CanCallGrpcServiceAdd()
        {
            _patientGrpcClientMock!
                .Setup(m => m.AddAsync(It.IsAny<PatientModelMessage>()))
                .Verifiable();
            IActionResult actionResult = _patientController!.Add(new PatientModel
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            }).Result;
            _patientGrpcClientMock.Verify();
        }

        [Test]
        public void Update_CanCallGrpcServiceUpdate()
        {
            _patientGrpcClientMock!
                .Setup(m => m.UpdateAsync(It.IsAny<UpdateMessage>()))
                .Verifiable();
            IActionResult actionResult = _patientController!.Update(It.IsAny<int>(), new PatientModel
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            }).Result;
            _patientGrpcClientMock.Verify();
        }

        [Test]
        public void Delete_CanCallGrpcServiceDelete()
        {
            _patientGrpcClientMock!
                .Setup(m => m.DeleteAsync(It.IsAny<IdMessage>()))
                .Verifiable();
            IActionResult actionResult = _patientController!.Delete(It.IsAny<int>()).Result;
            _patientGrpcClientMock.Verify();
        }
    }
}