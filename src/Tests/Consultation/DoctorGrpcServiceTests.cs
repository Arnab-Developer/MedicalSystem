//using Grpc.Core;
//using MedicalSystem.Services.Consultation.Protos;
//using MedicalSystem.Services.Consultation.ViewModels;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using GrpcServices = MedicalSystem.Services.Consultation.GrpcServices;

//namespace MedicalSystem.Tests.Services.Consultation
//{
//    /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/doctorControllerTests/*'/>
//    internal class DoctorGrpcServiceTests
//    {
//        private GrpcServices::DoctorService? _doctorGrpcService;
//        private Mock<IDoctorService>? _doctorServiceMock;
//        private Mock<ServerCallContext>? _serverCallContextMock;

//        /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/setup/*'/>
//        [SetUp]
//        public void Setup()
//        {
//            _doctorServiceMock = new Mock<IDoctorService>();
//            _doctorGrpcService = new GrpcServices::DoctorService(_doctorServiceMock.Object);
//            _serverCallContextMock = new Mock<ServerCallContext>();
//        }

//        /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/getAll_GivenValidViewModels_ReturnsValidViewModels/*'/>
//        [Test]
//        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
//        {
//            var doctorViewModels = new List<DoctorViewModel>()
//            {
//                new DoctorViewModel()
//                {
//                    Id = 1,
//                    FirstName = "doc1 f",
//                    LastName = "doc1 l"
//                },
//                new DoctorViewModel()
//                {
//                    Id = 2,
//                    FirstName = "doc2 f",
//                    LastName = "doc2 l"
//                }
//            };
//            _doctorServiceMock!.Setup(service => service.GetAll()).Returns(doctorViewModels);

//            DoctorModelsMessage doctorModelsMessage = _doctorGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;

//            Assert.AreEqual(1, doctorModelsMessage.Doctors[0].Id);
//            Assert.AreEqual("doc1 f", doctorModelsMessage.Doctors[0].FirstName);
//            Assert.AreEqual("doc1 l", doctorModelsMessage.Doctors[0].LastName);

//            Assert.AreEqual(2, doctorModelsMessage.Doctors[1].Id);
//            Assert.AreEqual("doc2 f", doctorModelsMessage.Doctors[1].FirstName);
//            Assert.AreEqual("doc2 l", doctorModelsMessage.Doctors[1].LastName);
//        }

//        /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/getAll_GivenEmptyViewModels_ReturnsEmptyViewModels/*'/>
//        [Test]
//        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
//        {
//            var doctorViewModels = new List<DoctorViewModel>();
//            _doctorServiceMock!.Setup(service => service.GetAll()).Returns(doctorViewModels);
//            DoctorModelsMessage doctorModelsMessage = _doctorGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;
//            Assert.Zero(doctorModelsMessage.Doctors.Count());
//        }

//        /// <include file='docs.xml' path='docs/members[@name="DoctorControllerTests"]/getAll_GivenException_ExpectException/*'/>
//        [Test]
//        public void GetAll_GivenException_ExpectException()
//        {
//            _doctorServiceMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>(); ;
//            Assert.Throws<NullReferenceException>(() => _doctorGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object));
//        }
//    }
//}
