using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Dals
{
    public interface IConsultationDal
    {
        IEnumerable<ConsultationDomainModel> GetAll();
        ConsultationDomainModel? GetById(int id);
        void Add(ConsultationDomainModel consultationDomainModel);
        void Update(ConsultationDomainModel consultationDomainModel);
        void Delete(ConsultationDomainModel consultationDomainModel);
    }
}
