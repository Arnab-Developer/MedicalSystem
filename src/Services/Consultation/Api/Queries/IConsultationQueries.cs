using MedicalSystem.Services.Consultation.Api.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Api.Queries
{
    public interface IConsultationQueries
    {
        IEnumerable<ConsultationViewModel> GetAll();

        ConsultationViewModel? GetById(int id);
    }
}
