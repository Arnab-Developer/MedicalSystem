using MedicalSystem.Services.Consultation.Domain;
using MedicalSystem.Services.Consultation.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Infrastructure
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly ConsultationContext _consultationContext;

        public ConsultationRepository(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        IUnitOfWork IRepository<ConsultationDomainModel>.UnitOfWork
        {
            get
            {
                return _consultationContext;
            }
        }

        ConsultationDomainModel? IConsultationRepository.GetById(int id)
        {
            ConsultationDomainModel? consultationDomainModel = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patient)
                .FirstOrDefault(consultation => consultation.Id == id);

            return consultationDomainModel;
        }

        void IConsultationRepository.Add(ConsultationDomainModel consultation)
        {
            _consultationContext.Consultations!.Add(consultation);
        }

        void IConsultationRepository.Update(int id, ConsultationDomainModel consultation)
        {

        }

        void IConsultationRepository.Delete(ConsultationDomainModel consultationDomainModel)
        {
            _consultationContext.Consultations!.Remove(consultationDomainModel);
        }
    }
}
