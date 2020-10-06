using MediatR;

namespace MedicalSystem.Services.Consultation.Commands
{
    internal class DeleteConsultationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
