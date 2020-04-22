using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Dals
{
    internal class PatentDal : IPatentDal
    {
        private readonly ConsultationContext _consultationContext;

        public PatentDal(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        public IEnumerable<PatentDomainModel> GetAll()
        {
            var patentDomainModels = _consultationContext.Patents.OrderBy(patent => patent.FirstName);
            return patentDomainModels;
        }
    }
}
