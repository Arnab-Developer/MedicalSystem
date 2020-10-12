using MediatR;
using MedicalSystem.Services.Consultation.Api.ViewModels;

namespace MedicalSystem.Services.Consultation.Api.Commands
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
