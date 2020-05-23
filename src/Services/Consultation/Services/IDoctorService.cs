using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <include file='docs.xml' path='docs/members[@name="IDoctorService"]/iDoctorService/*'/>
    public interface IDoctorService
    {
        /// <include file='docs.xml' path='docs/members[@name="IDoctorService"]/getAll/*'/>
        IEnumerable<DoctorViewModel> GetAll();
    }
}
