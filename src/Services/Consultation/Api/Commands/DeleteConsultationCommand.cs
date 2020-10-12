using MediatR;

namespace MedicalSystem.Services.Consultation.Api.Commands
{
    internal class DeleteConsultationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
