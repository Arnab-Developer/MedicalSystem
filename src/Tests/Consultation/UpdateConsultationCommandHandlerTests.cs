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
    internal class UpdateConsultationCommandHandlerTests
    {
        private Mock<IConsultationRepository>? _consultationRepositoryMock;
        private IRequestHandler<UpdateConsultationCommand, bool>? _updateConsultationCommandHandler;

        [SetUp]
        public void Setup()
        {
            _consultationRepositoryMock = new Mock<IConsultationRepository>();
            _updateConsultationCommandHandler = new UpdateConsultationCommandHandler(_consultationRepositoryMock.Object);
        }

        [Test]
        public void UpdateConsultationCommandHandler_GivenValid_ReturnsValid()
        {
            var updateConsultationCommand = new UpdateConsultationCommand
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
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };
            _consultationRepositoryMock!.Setup(mock => mock.GetById(It.IsAny<int>())).Returns(consultationDomainModel);
            _consultationRepositoryMock!.Setup(mock => mock.Update(It.IsAny<int>(), It.IsAny<ConsultationDomainModel>())).Verifiable();
            _consultationRepositoryMock!.Setup(mock => mock.UnitOfWork.SaveChanges()).Verifiable();
            _updateConsultationCommandHandler!.Handle(updateConsultationCommand, It.IsAny<CancellationToken>());
            _consultationRepositoryMock.Verify();
        }
    }
}
