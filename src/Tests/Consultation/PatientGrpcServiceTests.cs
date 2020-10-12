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
    internal class PatientGrpcServiceTests
    {
        private PatientService? _patientGrpcService;
        private Mock<IPatientQueries>? _patientQueriesMock;
        private Mock<ServerCallContext>? _serverCallContextMock;

        [SetUp]
        public void Setup()
        {
            _patientQueriesMock = new Mock<IPatientQueries>();
            _patientGrpcService = new PatientService(_patientQueriesMock.Object);
            _serverCallContextMock = new Mock<ServerCallContext>();
        }

        [Test]
        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
        {
            var patientViewModels = new List<PatientViewModel>()
            {
                new PatientViewModel()
                {
                    Id = 1,
                    FirstName = "pat1 f",
                    LastName = "pat1 l"
                },
                new PatientViewModel()
                {
                    Id = 2,
                    FirstName = "pat2 f",
                    LastName = "pat2 l"
                }
            };
            _patientQueriesMock!.Setup(service => service.GetAll()).Returns(patientViewModels);

            PatientModelsMessage patientModelsMessage = _patientGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;

            Assert.AreEqual(1, patientModelsMessage.Patients[0].Id);
            Assert.AreEqual("pat1 f", patientModelsMessage.Patients[0].FirstName);
            Assert.AreEqual("pat1 l", patientModelsMessage.Patients[0].LastName);

            Assert.AreEqual(2, patientModelsMessage.Patients[1].Id);
            Assert.AreEqual("pat2 f", patientModelsMessage.Patients[1].FirstName);
            Assert.AreEqual("pat2 l", patientModelsMessage.Patients[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
        {
            var patientViewModels = new List<PatientViewModel>();
            _patientQueriesMock!.Setup(service => service.GetAll()).Returns(patientViewModels);
            PatientModelsMessage patientModelsMessage = _patientGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;
            Assert.Zero(patientModelsMessage.Patients.Count());
        }

        [Test]
        public void GetAll_GivenException_ExpectException()
        {
            _patientQueriesMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>();
            Assert.Throws<NullReferenceException>(() => _patientGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object));
        }
    }
}
