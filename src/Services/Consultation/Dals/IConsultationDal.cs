using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="IConsultationDal"]/iConsultationDal/*'/>
    internal interface IConsultationDal
    {
        /// <include file='docs.xml' path='docs/members[@name="IConsultationDal"]/getAll/*'/>
        IEnumerable<ConsultationDomainModel> GetAll();

        /// <include file='docs.xml' path='docs/members[@name="IConsultationDal"]/getById/*'/>
        ConsultationDomainModel? GetById(int id);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationDal"]/add/*'/>
        void Add(ConsultationDomainModel consultationDomainModel);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationDal"]/update/*'/>
        void Update(ConsultationDomainModel consultationDomainModel);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationDal"]/delete/*'/>
        void Delete(ConsultationDomainModel consultationDomainModel);
    }
}
