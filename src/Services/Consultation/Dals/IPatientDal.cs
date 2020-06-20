using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="IPatientDal"]/iPatientDal/*'/>
    internal interface IPatientDal
    {
        /// <include file='docs.xml' path='docs/members[@name="IPatientDal"]/getAll/*'/>
        IEnumerable<PatientDomainModel> GetAll();
    }
}
