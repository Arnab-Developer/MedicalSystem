using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="PatientDal"]/patientDal/*'/>
    internal class PatientDal : IPatientDal
    {
        private readonly ConsultationContext _consultationContext;

        /// <include file='docs.xml' path='docs/members[@name="PatientDal"]/patientDalConstructor/*'/>
        public PatientDal(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientDal"]/getAll/*'/>
        IEnumerable<PatientDomainModel> IPatientDal.GetAll()
        {
            var patientDomainModels = _consultationContext.Patients.OrderBy(patient => patient.FirstName);
            return patientDomainModels;
        }
    }
}
