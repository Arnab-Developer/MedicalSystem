using MedicalSystem.Services.Consultation.SeedWork;

namespace MedicalSystem.Services.Consultation.DomainModels
{
    internal interface IConsultationRepository : IRepository<ConsultationDomainModel>
    {
        ConsultationDomainModel GetById(int id);
        
        void Add(ConsultationDomainModel consultation);
        
        void Update(int id, ConsultationDomainModel consultation);
        
        void Delete(ConsultationDomainModel consultationDomainModel);
    }
}
