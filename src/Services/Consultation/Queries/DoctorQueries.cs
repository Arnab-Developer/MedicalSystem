using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Queries
{
    public class DoctorQueries : IDoctorQueries
    {
        private readonly ConsultationContext _consultationContext;

        public DoctorQueries(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        public IEnumerable<DoctorViewModel> GetAll()
        {
            IOrderedQueryable<DoctorDomainModel> doctorDomainModels = _consultationContext.Doctors.OrderBy(doctor => doctor.FirstName);
            var doctorViewModels = new List<DoctorViewModel>();
            foreach (DoctorDomainModel doctorDomainModel in doctorDomainModels)
            {
                var doctorViewModel = new DoctorViewModel()
                {
                    Id = doctorDomainModel.Id,
                    FirstName = doctorDomainModel.FirstName,
                    LastName = doctorDomainModel.LastName
                };
                doctorViewModels.Add(doctorViewModel);
            }
            return doctorViewModels;
        }
    }
}
