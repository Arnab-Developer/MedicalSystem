using MedicalSystem.Services.Consultation.Api.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Api.Queries
{
    public interface IPatientQueries
    {
        IEnumerable<PatientViewModel> GetAll();
    }
}
