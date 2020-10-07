using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Queries
{
    public interface IConsultationQueries
    {
        IEnumerable<ConsultationViewModel> GetAll();

        ConsultationViewModel? GetById(int id);
    }
}
