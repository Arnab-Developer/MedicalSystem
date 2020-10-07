﻿using MediatR;
using MedicalSystem.Services.Consultation.DomainModels;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.Commands
{
    internal class AddConsultationCommandHandler : IRequestHandler<AddConsultationCommand, bool>
    {
        private readonly IConsultationRepository _consultationRepository;

        public AddConsultationCommandHandler(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        public Task<bool> Handle(AddConsultationCommand request, CancellationToken cancellationToken)
        {
            var consultationDomainModel = new ConsultationDomainModel(request.ConsultationViewModel.Id, request.ConsultationViewModel.Date,
                request.ConsultationViewModel.Country, request.ConsultationViewModel.State, request.ConsultationViewModel.City,
                request.ConsultationViewModel.PinCode, request.ConsultationViewModel.Problem, request.ConsultationViewModel.Medicine,
                request.ConsultationViewModel.DoctorId, request.ConsultationViewModel.PatientId);
            _consultationRepository.Add(consultationDomainModel);
            _consultationRepository.UnitOfWork.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
