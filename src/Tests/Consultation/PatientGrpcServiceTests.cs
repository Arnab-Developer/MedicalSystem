using Grpc.Core;
using MedicalSystem.Services.Consultation.Protos;
using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using GrpcServices = MedicalSystem.Services.Consultation.GrpcServices;

namespace MedicalSystem.Tests.Services.Consultation
{
    /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/patientControllerTests/*'/>
    internal class PatientGrpcServiceTests
    {
        private GrpcServices::PatientService? _patientGrpcService;
        private Mock<IPatientService>? _patientServiceMock;
        private Mock<ServerCallContext>? _serverCallContextMock;

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/setup/*'/>
        [SetUp]
        public void Setup()
        {
            _patientServiceMock = new Mock<IPatientService>();
            _patientGrpcService = new GrpcServices::PatientService(_patientServiceMock.Object);
            _serverCallContextMock = new Mock<ServerCallContext>();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getAll_GivenValidViewModels_ReturnsValidViewModels/*'/>
        [Test]
        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
        {
            var patientViewModels = new List<PatientViewModel>()
            {
                new PatientViewModel()
                {
                    Id = 1,
                    FirstName = "doc1 f",
                    LastName = "doc1 l"
                },
                new PatientViewModel()
                {
                    Id = 2,
                    FirstName = "doc2 f",
                    LastName = "doc2 l"
                }
            };
            _patientServiceMock!.Setup(service => service.GetAll()).Returns(patientViewModels);

            PatientModelsMessage patientModelsMessage = _patientGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;

            Assert.AreEqual(1, patientModelsMessage.Patients[0].Id);
            Assert.AreEqual("doc1 f", patientModelsMessage.Patients[0].FirstName);
            Assert.AreEqual("doc1 l", patientModelsMessage.Patients[0].LastName);

            Assert.AreEqual(2, patientModelsMessage.Patients[1].Id);
            Assert.AreEqual("doc2 f", patientModelsMessage.Patients[1].FirstName);
            Assert.AreEqual("doc2 l", patientModelsMessage.Patients[1].LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getAll_GivenEmptyViewModels_ReturnsEmptyViewModels/*'/>
        [Test]
        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
        {
            var patientViewModels = new List<PatientViewModel>();
            _patientServiceMock!.Setup(service => service.GetAll()).Returns(patientViewModels);
            PatientModelsMessage patientModelsMessage = _patientGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;
            Assert.Zero(patientModelsMessage.Patients.Count());
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getAll_GivenException_ExpectException/*'/>
        [Test]
        public void GetAll_GivenException_ExpectException()
        {
            _patientServiceMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>(); ;
            Assert.Throws<NullReferenceException>(() => _patientGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object));
        }
    }
}
