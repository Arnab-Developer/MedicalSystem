using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <include file='docs.xml' path='docs/members[@name="PatientService"]/patientService/*'/>
    internal class PatientService : IPatientService
    {
        private readonly IPatientDal _patientDal;

        /// <include file='docs.xml' path='docs/members[@name="PatientService"]/patientServiceConstructor/*'/>
        public PatientService(IPatientDal patientDal)
        {
            _patientDal = patientDal;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientService"]/getAll/*'/>
        IEnumerable<PatientViewModel> IPatientService.GetAll()
        {
            var patientDomainModels = _patientDal.GetAll();
            var patientViewModels = new List<PatientViewModel>();
            foreach (var patientDomainModel in patientDomainModels)
            {
                var patientViewModel = new PatientViewModel()
                {
                    Id = patientDomainModel.Id,
                    FirstName = patientDomainModel.FirstName,
                    LastName = patientDomainModel.LastName
                };
                patientViewModels.Add(patientViewModel);
            }
            return patientViewModels;
        }
    }
}
