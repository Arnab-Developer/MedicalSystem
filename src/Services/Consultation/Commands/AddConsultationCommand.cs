using MediatR;
using MedicalSystem.Services.Consultation.ViewModels;

namespace MedicalSystem.Services.Consultation.Commands
{
    internal class AddConsultationCommand : IRequest<bool>
    {
        public ConsultationViewModel ConsultationViewModel { get; set; }

        public AddConsultationCommand()
        {
            ConsultationViewModel = new ConsultationViewModel();
        }
    }
}
