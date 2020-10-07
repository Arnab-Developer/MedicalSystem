using MediatR;
using MedicalSystem.Services.Consultation.DomainModels;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.Commands
{
    internal class DeleteConsultationCommandHandler : IRequestHandler<DeleteConsultationCommand, bool>
    {
        private readonly IConsultationRepository _consultationRepository;

        public DeleteConsultationCommandHandler(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        public Task<bool> Handle(DeleteConsultationCommand request, CancellationToken cancellationToken)
        {
            ConsultationDomainModel consultationDomainModel = _consultationRepository.GetById(request.Id);

            if (consultationDomainModel == null)
            {
                return Task.FromResult(false);
            }

            _consultationRepository.Delete(consultationDomainModel);
            _consultationRepository.UnitOfWork.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
