using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    public interface IDoctorService
    {
        IEnumerable<DoctorViewModel> GetAll();
    }
}
