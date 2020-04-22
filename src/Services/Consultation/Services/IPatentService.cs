using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    public interface IPatentService
    {
        IEnumerable<PatentViewModel> GetAll();
    }
}
