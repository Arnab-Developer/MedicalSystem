using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <include file='docs.xml' path='docs/members[@name="IPatentService"]/iPatentService/*'/>
    public interface IPatentService
    {
        /// <include file='docs.xml' path='docs/members[@name="IPatentService"]/getAll/*'/>
        IEnumerable<PatentViewModel> GetAll();
    }
}
