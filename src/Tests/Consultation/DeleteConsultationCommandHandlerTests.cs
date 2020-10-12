using MediatR;
using MedicalSystem.Services.Consultation.Api.Commands;
using MedicalSystem.Services.Consultation.Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class DeleteConsultationCommandHandlerTests
    {
        private Mock<IConsultationRepository>? _consultationRepositoryMock;
        private IRequestHandler<DeleteConsultationCommand, bool>? _deleteConsultationCommandHandler;

        [SetUp]
        public void Setup()
        {
            _consultationRepositoryMock = new Mock<IConsultationRepository>();
            _deleteConsultationCommandHandler = new DeleteConsultationCommandHandler(_consultationRepositoryMock.Object);
        }

        [Test]
        public void UpdateConsultationCommandHandler_GivenValid_ReturnsValid()
        {
            var deleteConsultationCommand = new DeleteConsultationCommand
            {
                Id = 10
            };
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };
            _consultationRepositoryMock!.Setup(mock => mock.GetById(It.IsAny<int>())).Returns(consultationDomainModel);
            _consultationRepositoryMock!.Setup(mock => mock.Delete(It.IsAny<ConsultationDomainModel>())).Verifiable();
            _consultationRepositoryMock!.Setup(mock => mock.UnitOfWork.SaveChanges()).Verifiable();
            _deleteConsultationCommandHandler!.Handle(deleteConsultationCommand, It.IsAny<CancellationToken>());
            _consultationRepositoryMock.Verify();
        }
    }
}
