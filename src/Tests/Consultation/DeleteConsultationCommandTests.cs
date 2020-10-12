using MedicalSystem.Services.Consultation.Api.Commands;
using NUnit.Framework;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class DeleteConsultationCommandTests
    {
        [Test]
        public void DeleteConsultationCommand_GivenValid_ReturnsValid()
        {
            var deleteConsultationCommand = new DeleteConsultationCommand
            {
                Id = 10
            };
            Assert.AreEqual(10, deleteConsultationCommand.Id);
        }
    }
}
