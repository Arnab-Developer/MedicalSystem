using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <include file='docs.xml' path='docs/members[@name="PatentService"]/patentService/*'/>
    internal class PatentService : IPatentService
    {
        private readonly IPatentDal _patentDal;

        /// <include file='docs.xml' path='docs/members[@name="PatentService"]/patentServiceConstructor/*'/>
        public PatentService(IPatentDal patentDal)
        {
            _patentDal = patentDal;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentService"]/getAll/*'/>
        public IEnumerable<PatentViewModel> GetAll()
        {
            var patentDomainModels = _patentDal.GetAll();
            var patentViewModels = new List<PatentViewModel>();
            foreach (var patentDomainModel in patentDomainModels)
            {
                var patentViewModel = new PatentViewModel()
                {
                    Id = patentDomainModel.Id,
                    FirstName = patentDomainModel.FirstName,
                    LastName = patentDomainModel.LastName
                };
                patentViewModels.Add(patentViewModel);
            }
            return patentViewModels;
        }
    }
}
