using MediatR;
using MedicalSystem.Services.Consultation.Api.ViewModels;

namespace MedicalSystem.Services.Consultation.Api.Commands
{
    internal class UpdateConsultationCommand : IRequest<bool>
    {
        public ConsultationViewModel ConsultationViewModel { get; set; }

        public UpdateConsultationCommand()
        {
            ConsultationViewModel = new ConsultationViewModel();
        }
    }
}
