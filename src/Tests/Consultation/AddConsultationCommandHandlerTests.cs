using MediatR;
using MedicalSystem.Services.Consultation.Api.Commands;
using MedicalSystem.Services.Consultation.Api.ViewModels;
using MedicalSystem.Services.Consultation.Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class AddConsultationCommandHandlerTests
    {
        private Mock<IConsultationRepository>? _consultationRepositoryMock;
        private IRequestHandler<AddConsultationCommand, bool>? _addConsultationCommandHandler;

        [SetUp]
        public void Setup()
        {
            _consultationRepositoryMock = new Mock<IConsultationRepository>();
            _addConsultationCommandHandler = new AddConsultationCommandHandler(_consultationRepositoryMock.Object);
        }

        [Test]
        public void AddConsultationCommandHandler_GivenValid_ReturnsValid()
        {
            var addConsultationCommand = new AddConsultationCommand
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
            _consultationRepositoryMock!.Setup(mock => mock.Add(It.IsAny<ConsultationDomainModel>())).Verifiable();
            _consultationRepositoryMock!.Setup(mock => mock.UnitOfWork.SaveChanges()).Verifiable();
            _addConsultationCommandHandler!.Handle(addConsultationCommand, It.IsAny<CancellationToken>());
            _consultationRepositoryMock.Verify();
        }
    }
}
