using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Queries
{
    public interface IPatientQueries
    {
        IEnumerable<PatientViewModel> GetAll();
    }
}
