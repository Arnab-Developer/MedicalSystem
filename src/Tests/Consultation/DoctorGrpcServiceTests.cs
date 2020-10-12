using Grpc.Core;
using MedicalSystem.Services.Consultation.Api.GrpcServices;
using MedicalSystem.Services.Consultation.Api.Protos;
using MedicalSystem.Services.Consultation.Api.Queries;
using MedicalSystem.Services.Consultation.Api.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class DoctorGrpcServiceTests
    {
        private DoctorService? _doctorGrpcService;
        private Mock<IDoctorQueries>? _doctorQueriesMock;
        private Mock<ServerCallContext>? _serverCallContextMock;

        [SetUp]
        public void Setup()
        {
            _doctorQueriesMock = new Mock<IDoctorQueries>();
            _doctorGrpcService = new DoctorService(_doctorQueriesMock.Object);
            _serverCallContextMock = new Mock<ServerCallContext>();
        }

        [Test]
        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
        {
            var doctorViewModels = new List<DoctorViewModel>()
            {
                new DoctorViewModel()
                {
                    Id = 1,
                    FirstName = "doc1 f",
                    LastName = "doc1 l"
                },
                new DoctorViewModel()
                {
                    Id = 2,
                    FirstName = "doc2 f",
                    LastName = "doc2 l"
                }
            };
            _doctorQueriesMock!.Setup(service => service.GetAll()).Returns(doctorViewModels);

            DoctorModelsMessage doctorModelsMessage = _doctorGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;

            Assert.AreEqual(1, doctorModelsMessage.Doctors[0].Id);
            Assert.AreEqual("doc1 f", doctorModelsMessage.Doctors[0].FirstName);
            Assert.AreEqual("doc1 l", doctorModelsMessage.Doctors[0].LastName);

            Assert.AreEqual(2, doctorModelsMessage.Doctors[1].Id);
            Assert.AreEqual("doc2 f", doctorModelsMessage.Doctors[1].FirstName);
            Assert.AreEqual("doc2 l", doctorModelsMessage.Doctors[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
        {
            var doctorViewModels = new List<DoctorViewModel>();
            _doctorQueriesMock!.Setup(service => service.GetAll()).Returns(doctorViewModels);
            DoctorModelsMessage doctorModelsMessage = _doctorGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;
            Assert.Zero(doctorModelsMessage.Doctors.Count());
        }

        [Test]
        public void GetAll_GivenException_ExpectException()
        {
            _doctorQueriesMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>();
            Assert.Throws<NullReferenceException>(() => _doctorGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object));
        }
    }
}
