using MediatR;
using MedicalSystem.Services.Consultation.ViewModels;

namespace MedicalSystem.Services.Consultation.Commands
{
    public class UpdateConsultationCommand : IRequest<bool>
    {
        public ConsultationViewModel ConsultationViewModel { get; set; }

        public UpdateConsultationCommand()
        {
            ConsultationViewModel = new ConsultationViewModel();
        }
    }
}
