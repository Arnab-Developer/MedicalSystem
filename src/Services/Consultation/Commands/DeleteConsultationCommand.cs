using MediatR;

namespace MedicalSystem.Services.Consultation.Commands
{
    public class DeleteConsultationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
