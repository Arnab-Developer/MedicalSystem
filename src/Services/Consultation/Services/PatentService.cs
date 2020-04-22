using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    internal class PatentService : IPatentService
    {
        private readonly IPatentDal _patentDal;

        public PatentService(IPatentDal patentDal)
        {
            _patentDal = patentDal;
        }

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
