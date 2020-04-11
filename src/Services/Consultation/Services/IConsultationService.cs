using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    public interface IConsultationService
    {
        IEnumerable<ConsultationViewModel> GetAll();
        ConsultationViewModel? GetById(int id);
        void Add(ConsultationViewModel consultation);
        void Update(int id, ConsultationViewModel consultation);
        void Delete(int id);
    }
}
