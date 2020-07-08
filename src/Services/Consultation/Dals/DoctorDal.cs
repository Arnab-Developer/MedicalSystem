using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorDal"]/doctorDal/*'/>
    internal class DoctorDal : IDoctorDal
    {
        private readonly ConsultationContext _consultationContext;

        /// <include file='docs.xml' path='docs/members[@name="DoctorDal"]/doctorDalConstructor/*'/>
        public DoctorDal(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorDal"]/getAll/*'/>
        IEnumerable<DoctorDomainModel> IDoctorDal.GetAll()
        {
            IOrderedQueryable<DoctorDomainModel> doctorDomainModels = _consultationContext.Doctors.OrderBy(doctor => doctor.FirstName);
            return doctorDomainModels;
        }
    }
}
