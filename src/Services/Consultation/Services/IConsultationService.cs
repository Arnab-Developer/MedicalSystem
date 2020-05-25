using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <include file='docs.xml' path='docs/members[@name="IConsultationService"]/iConsultationService/*'/>
    public interface IConsultationService
    {
        /// <include file='docs.xml' path='docs/members[@name="IConsultationService"]/iConsultationService/*'/>
        IEnumerable<ConsultationViewModel> GetAll();

        /// <include file='docs.xml' path='docs/members[@name="IConsultationService"]/getById/*'/>
        ConsultationViewModel? GetById(int id);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationService"]/add/*'/>
        void Add(ConsultationViewModel consultation);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationService"]/update/*'/>
        void Update(int id, ConsultationViewModel consultation);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationService"]/delete/*'/>
        void Delete(int id);
    }
}
