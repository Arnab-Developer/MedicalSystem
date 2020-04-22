using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Dals
{
    internal interface IDoctorDal
    {
        IEnumerable<DoctorDomainModel> GetAll();
    }
}
