using MediatR;
using MedicalSystem.Services.Consultation.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.Api.Commands
{
    internal class UpdateConsultationCommandHandler : IRequestHandler<UpdateConsultationCommand, bool>
    {
        private readonly IConsultationRepository _consultationRepository;

        public UpdateConsultationCommandHandler(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        Task<bool> IRequestHandler<UpdateConsultationCommand, bool>.Handle(UpdateConsultationCommand request, CancellationToken cancellationToken)
        {
            ConsultationDomainModel? consultationDomainModel = _consultationRepository.GetById(request.ConsultationViewModel.Id);

            if (consultationDomainModel == null)
            {
                return Task.FromResult(false);
            }

            consultationDomainModel.Date = request.ConsultationViewModel.Date;
            consultationDomainModel.Place = new Place(request.ConsultationViewModel.Country,
                request.ConsultationViewModel.State, request.ConsultationViewModel.City, request.ConsultationViewModel.PinCode);
            consultationDomainModel.Problem = request.ConsultationViewModel.Problem;
            consultationDomainModel.Medicine = request.ConsultationViewModel.Medicine;
            consultationDomainModel.DoctorId = request.ConsultationViewModel.DoctorId;
            consultationDomainModel.PatientId = request.ConsultationViewModel.PatientId;

            _consultationRepository.Update(request.ConsultationViewModel.Id, consultationDomainModel);
            _consultationRepository.UnitOfWork.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
