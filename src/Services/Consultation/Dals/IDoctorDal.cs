using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="IDoctorDal"]/iDoctorDal/*'/>
    internal interface IDoctorDal
    {
        /// <include file='docs.xml' path='docs/members[@name="IDoctorDal"]/getAll/*'/>
        IEnumerable<DoctorDomainModel> GetAll();
    }
}
