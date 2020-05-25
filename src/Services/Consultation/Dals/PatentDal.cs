using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="PatentDal"]/patentDal/*'/>
    internal class PatentDal : IPatentDal
    {
        private readonly ConsultationContext _consultationContext;

        /// <include file='docs.xml' path='docs/members[@name="PatentDal"]/patentDalConstructor/*'/>
        public PatentDal(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentDal"]/getAll/*'/>
        public IEnumerable<PatentDomainModel> GetAll()
        {
            var patentDomainModels = _consultationContext.Patents.OrderBy(patent => patent.FirstName);
            return patentDomainModels;
        }
    }
}
