using MedicalSystem.Gateways.WebGateway.Controllers;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors;
using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Protos.Doctors;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Tests.Gateways.WebGateway
{
    internal class DoctorControllerTests
    {
        private Mock<IDoctorGrpcClient>? _doctorGrpcClientMock;
        private DoctorController? _doctorController;

        [SetUp]
        public void Setup()
        {
            _doctorGrpcClientMock = new Mock<IDoctorGrpcClient>();
            _doctorController = new DoctorController(_doctorGrpcClientMock.Object);
        }

        [Test]
        public void GetAll_GivenValidModelsMessage_ReturnsValidModels()
        {
            var doctorModelsMessage = new DoctorModelsMessage();
            doctorModelsMessage.Doctors.Add(new DoctorModelMessage
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            });
            _doctorGrpcClientMock!
                .Setup(m => m.GetAllAsync(It.IsAny<EmptyMessage>()))
                .Returns(Task.FromResult(doctorModelsMessage));

            IList<DoctorModel> doctorModels
                = ((IEnumerable<DoctorModel>)((OkObjectResult)_doctorController!.GetAll().Result.Result).Value).ToList();

            Assert.AreEqual(doctorModelsMessage.Doctors[0].Id, doctorModels[0].Id);
            Assert.AreEqual(doctorModelsMessage.Doctors[0].FirstName, doctorModels[0].FirstName);
            Assert.AreEqual(doctorModelsMessage.Doctors[0].LastName, doctorModels[0].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyModelsMessage_ReturnsErrorModels()
        {
            var doctorModelsMessage = new DoctorModelsMessage();
            _doctorGrpcClientMock!
                .Setup(m => m.GetAllAsync(It.IsAny<EmptyMessage>()))
                .Returns(Task.FromResult(doctorModelsMessage));

            ErrorModel error
                = (ErrorModel)((NotFoundObjectResult)_doctorController!.GetAll().Result.Result).Value;

            Assert.AreEqual("No doctor record found.", error.Reason);
        }

        [Test]
        public void GetById_GivenValidModelMessage_ReturnsValidModel()
        {
            var doctorModelMessage = new DoctorModelMessage
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            };
            _doctorGrpcClientMock!
                .Setup(m => m.GetByIdAsync(It.IsAny<IdMessage>()))
                .Returns(Task.FromResult(doctorModelMessage));

            DoctorModel doctorModel
                = (DoctorModel)((OkObjectResult)_doctorController!.GetById(It.IsAny<int>()).Result.Result).Value;

            Assert.AreEqual(doctorModelMessage.Id, doctorModel.Id);
            Assert.AreEqual(doctorModelMessage.FirstName, doctorModel.FirstName);
            Assert.AreEqual(doctorModelMessage.LastName, doctorModel.LastName);
        }

        [Test]
        public void GetById_GivenEmptyModelsMessage_ReturnsErrorModels()
        {
            _doctorGrpcClientMock!
                .Setup(m => m.GetByIdAsync(It.IsAny<IdMessage>()))
                .Returns(Task.FromResult(It.IsAny<DoctorModelMessage>()));

            ErrorModel error
                = (ErrorModel)((NotFoundObjectResult)_doctorController!.GetById(It.IsAny<int>()).Result.Result).Value;

            Assert.AreEqual("No doctor record found.", error.Reason);
        }

        [Test]
        public void Add_CanCallGrpcServiceAdd()
        {
            _doctorGrpcClientMock!
                .Setup(m => m.AddAsync(It.IsAny<DoctorModelMessage>()))
                .Verifiable();
            IActionResult actionResult = _doctorController!.Add(new DoctorModel
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            }).Result;
            _doctorGrpcClientMock.Verify();
        }

        [Test]
        public void Update_CanCallGrpcServiceUpdate()
        {
            _doctorGrpcClientMock!
                .Setup(m => m.UpdateAsync(It.IsAny<UpdateMessage>()))
                .Verifiable();
            IActionResult actionResult = _doctorController!.Update(It.IsAny<int>(), new DoctorModel
            {
                Id = 1,
                FirstName = "f1",
                LastName = "l1"
            }).Result;
            _doctorGrpcClientMock.Verify();
        }

        [Test]
        public void Delete_CanCallGrpcServiceDelete()
        {
            _doctorGrpcClientMock!
                .Setup(m => m.DeleteAsync(It.IsAny<IdMessage>()))
                .Verifiable();
            IActionResult actionResult = _doctorController!.Delete(It.IsAny<int>()).Result;
            _doctorGrpcClientMock.Verify();
        }
    }
}