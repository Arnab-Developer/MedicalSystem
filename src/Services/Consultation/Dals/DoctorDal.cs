using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Dals
{
    internal class DoctorDal : IDoctorDal
    {
        private readonly ConsultationContext _consultationContext;

        public DoctorDal(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        public IEnumerable<DoctorDomainModel> GetAll()
        {
            var doctorDomainModels = _consultationContext.Doctors.OrderBy(doctor => doctor.FirstName);
            return doctorDomainModels;
        }
    }
}
