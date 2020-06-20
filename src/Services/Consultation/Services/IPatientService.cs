using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <include file='docs.xml' path='docs/members[@name="IPatientService"]/iPatientService/*'/>
    public interface IPatientService
    {
        /// <include file='docs.xml' path='docs/members[@name="IPatientService"]/getAll/*'/>
        IEnumerable<PatientViewModel> GetAll();
    }
}
