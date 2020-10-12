using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using MedicalSystem.Services.Consultation.Api.Commands;
using MedicalSystem.Services.Consultation.Api.GrpcServices;
using MedicalSystem.Services.Consultation.Api.Protos;
using MedicalSystem.Services.Consultation.Api.Queries;
using MedicalSystem.Services.Consultation.Api.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class ConsultationGrpcServiceTests
    {
        private ConsultationService? _consultationGrpcService;
        private Mock<IConsultationQueries>? _consultationQueriesMock;
        private Mock<IMediator>? _mediatorMock;
        private Mock<ServerCallContext>? _serverCallContextMock;

        [SetUp]
        public void Setup()
        {
            _consultationQueriesMock = new Mock<IConsultationQueries>();
            _mediatorMock = new Mock<IMediator>();
            _consultationGrpcService = new ConsultationService(_consultationQueriesMock.Object, _mediatorMock.Object);
            _serverCallContextMock = new Mock<ServerCallContext>();
        }

        [Test]
        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
        {
            var consultationViewModels = new List<ConsultationViewModel>()
            {
                new ConsultationViewModel()
                {
                    Id = 1,
                    Date = DateTime.UtcNow,
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
                },
                new ConsultationViewModel()
                {
                    Id = 2,
                    Date = DateTime.UtcNow,
                    Country = "UK",
                    State = "st1",
                    City = "c1",
                    PinCode = "4567",
                    Problem = "p2",
                    Medicine = "m2",
                    DoctorId = 2,
                    Doctor = new DoctorViewModel()
                    {
                        Id = 2,
                        FirstName = "doc2 fir",
                        LastName = "doc2 las",
                    },
                    PatientId = 2,
                    Patient = new PatientViewModel()
                    {
                        Id = 2,
                        FirstName = "pat2 fir",
                        LastName = "pat2 las"
                    }
                }
            };
            _consultationQueriesMock!.Setup(service => service.GetAll()).Returns(consultationViewModels);

            ConsultationModelsMessage consultationModelsMessage
                = _consultationGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;

            Assert.AreEqual(2, consultationModelsMessage.Consultations.Count);

            Assert.AreEqual(1, consultationModelsMessage.Consultations[0].Id);
            Assert.AreEqual(DateTime.UtcNow.Date, consultationModelsMessage.Consultations[0].Date.ToDateTime().Date);
            Assert.AreEqual("India", consultationModelsMessage.Consultations[0].Country);
            Assert.AreEqual("WB", consultationModelsMessage.Consultations[0].State);
            Assert.AreEqual("Kol", consultationModelsMessage.Consultations[0].City);
            Assert.AreEqual("1234", consultationModelsMessage.Consultations[0].PinCode);
            Assert.AreEqual("P1", consultationModelsMessage.Consultations[0].Problem);
            Assert.AreEqual("M1", consultationModelsMessage.Consultations[0].Medicine);
            Assert.AreEqual(1, consultationModelsMessage.Consultations[0].DoctorId);
            Assert.AreEqual(1, consultationModelsMessage.Consultations[0].Doctor!.Id);
            Assert.AreEqual("doc1first", consultationModelsMessage.Consultations[0].Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationModelsMessage.Consultations[0].Doctor!.LastName);
            Assert.AreEqual(1, consultationModelsMessage.Consultations[0].PatientId);
            Assert.AreEqual(1, consultationModelsMessage.Consultations[0].Patient!.Id);
            Assert.AreEqual("pat1first", consultationModelsMessage.Consultations[0].Patient!.FirstName);
            Assert.AreEqual("pat1last", consultationModelsMessage.Consultations[0].Patient!.LastName);

            Assert.AreEqual(2, consultationModelsMessage.Consultations[1].Id);
            Assert.AreEqual(DateTime.UtcNow.Date, consultationModelsMessage.Consultations[1].Date.ToDateTime().Date);
            Assert.AreEqual("UK", consultationModelsMessage.Consultations[1].Country);
            Assert.AreEqual("st1", consultationModelsMessage.Consultations[1].State);
            Assert.AreEqual("c1", consultationModelsMessage.Consultations[1].City);
            Assert.AreEqual("4567", consultationModelsMessage.Consultations[1].PinCode);
            Assert.AreEqual("p2", consultationModelsMessage.Consultations[1].Problem);
            Assert.AreEqual("m2", consultationModelsMessage.Consultations[1].Medicine);
            Assert.AreEqual(2, consultationModelsMessage.Consultations[1].DoctorId);
            Assert.AreEqual(2, consultationModelsMessage.Consultations[1].Doctor!.Id);
            Assert.AreEqual("doc2 fir", consultationModelsMessage.Consultations[1].Doctor!.FirstName);
            Assert.AreEqual("doc2 las", consultationModelsMessage.Consultations[1].Doctor!.LastName);
            Assert.AreEqual(2, consultationModelsMessage.Consultations[1].PatientId);
            Assert.AreEqual(2, consultationModelsMessage.Consultations[1].Patient!.Id);
            Assert.AreEqual("pat2 fir", consultationModelsMessage.Consultations[1].Patient!.FirstName);
            Assert.AreEqual("pat2 las", consultationModelsMessage.Consultations[1].Patient!.LastName);
        }

        [Test]
        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
        {
            var consultationViewModels = new List<ConsultationViewModel>();
            _consultationQueriesMock!.Setup(service => service.GetAll()).Returns(consultationViewModels);

            ConsultationModelsMessage consultationModelsMessage
                = _consultationGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;

            Assert.Zero(consultationModelsMessage.Consultations.Count);
        }

        [Test]
        public void GetAll_GivenNullViewModels_ExpectException()
        {
            var consultationViewModels = new List<ConsultationViewModel>();
            _consultationQueriesMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>();

            Assert.Throws<NullReferenceException>(() => _consultationGrpcService!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object));
        }

        [Test]
        public void GetById_GivenValidViewModel_ReturnsValidViewModel()
        {
            var consultationViewModel = new ConsultationViewModel()
            {
                Id = 1,
                Date = DateTime.UtcNow,
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
            };
            _consultationQueriesMock!.Setup(service => service.GetById(It.IsAny<int>())).Returns(consultationViewModel);

            ConsultationModelMessage? consultationModelMessage
                = _consultationGrpcService!.GetById(new IdMessage { Id = It.IsAny<int>() }, _serverCallContextMock!.Object)!.Result;

            Assert.AreEqual(1, consultationModelMessage!.Id);
            Assert.AreEqual(DateTime.UtcNow.Date, consultationModelMessage.Date.ToDateTime().Date);
            Assert.AreEqual("India", consultationModelMessage.Country);
            Assert.AreEqual("WB", consultationModelMessage.State);
            Assert.AreEqual("Kol", consultationModelMessage.City);
            Assert.AreEqual("1234", consultationModelMessage.PinCode);
            Assert.AreEqual("P1", consultationModelMessage.Problem);
            Assert.AreEqual("M1", consultationModelMessage.Medicine);
            Assert.AreEqual(1, consultationModelMessage.DoctorId);
            Assert.AreEqual(1, consultationModelMessage.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationModelMessage.Doctor.FirstName);
            Assert.AreEqual("doc1last", consultationModelMessage.Doctor.LastName);
            Assert.AreEqual(1, consultationModelMessage.PatientId);
            Assert.AreEqual(1, consultationModelMessage.Patient!.Id);
            Assert.AreEqual("pat1first", consultationModelMessage.Patient.FirstName);
            Assert.AreEqual("pat1last", consultationModelMessage.Patient.LastName);
        }

        [Test]
        public void GetById_GivenNullViewModel_ReturnsNull()
        {
            _consultationQueriesMock!.Setup(service => service.GetById(It.IsAny<int>())).Returns<ConsultationViewModel>(null);
            Assert.Null(_consultationGrpcService!.GetById(new IdMessage { Id = It.IsAny<int>() }, _serverCallContextMock!.Object));
        }

        [Test]
        public void Add_CanCallServiceAdd()
        {
            var consultationModelMessage = new ConsultationModelMessage
            {
                Id = 1,
                Date = DateTime.Now.ToUniversalTime().ToTimestamp(),
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

            _mediatorMock!.Setup(mediator => mediator.Send(It.IsAny<AddConsultationCommand>(), It.IsAny<CancellationToken>())).Verifiable();
            _consultationGrpcService!.Add(consultationModelMessage, _serverCallContextMock!.Object);
            _mediatorMock.Verify();
        }

        [Test]
        public void Update_CanCallServiceUpdate()
        {
            var consultationModelMessage = new ConsultationModelMessage
            {
                Id = 1,
                Date = DateTime.Now.ToUniversalTime().ToTimestamp(),
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
            _mediatorMock!.Setup(mediator => mediator.Send(It.IsAny<UpdateConsultationCommand>(), It.IsAny<CancellationToken>())).Verifiable();
            _consultationGrpcService!.Update(new UpdateMessage { Id = It.IsAny<int>(), Consultation = consultationModelMessage },
                _serverCallContextMock!.Object);
            _mediatorMock.Verify();
        }

        [Test]
        public void Delete_CanCallServiceDelete()
        {
            _mediatorMock!.Setup(mediator => mediator.Send(It.IsAny<DeleteConsultationCommand>(), It.IsAny<CancellationToken>())).Verifiable();
            _consultationGrpcService!.Delete(new IdMessage { Id = It.IsAny<int>() }, _serverCallContextMock!.Object);
            _mediatorMock.Verify();
        }
    }
}
