using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Queries
{
    public class PatientQueries : IPatientQueries
    {
        private readonly ConsultationContext _consultationContext;

        public PatientQueries(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        public IEnumerable<PatientViewModel> GetAll()
        {
            IOrderedQueryable<PatientDomainModel> patientDomainModels = _consultationContext.Patients.OrderBy(doctor => doctor.FirstName);
            var patientViewModels = new List<PatientViewModel>();
            foreach (PatientDomainModel patientDomainModel in patientDomainModels)
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
