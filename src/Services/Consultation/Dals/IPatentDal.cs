using MedicalSystem.Services.Consultation.DomainModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="IPatentDal"]/iPatentDal/*'/>
    internal interface IPatentDal
    {
        /// <include file='docs.xml' path='docs/members[@name="IPatentDal"]/getAll/*'/>
        IEnumerable<PatentDomainModel> GetAll();
    }
}
