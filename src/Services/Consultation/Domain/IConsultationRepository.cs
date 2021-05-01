using MedicalSystem.Services.Consultation.Domain.SeedWork;

namespace MedicalSystem.Services.Consultation.Domain
{
    public interface IConsultationRepository : IRepository<ConsultationDomainModel>
    {
        ConsultationDomainModel? GetById(int id);

        void Add(ConsultationDomainModel consultation);

        void Update(int id, ConsultationDomainModel consultation);

        void Delete(ConsultationDomainModel consultationDomainModel);
    }
}
