using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <summary>
    /// Doctor service class.
    /// </summary>
    internal class DoctorService : IDoctorService
    {
        private readonly IDoctorDal _doctorDal;

        /// <summary>
        /// Create new doctor service object.
        /// </summary>
        /// <param name="doctorDal">Doctor dal.</param>
        public DoctorService(IDoctorDal doctorDal)
        {
            _doctorDal = doctorDal;
        }

        /// <summary>
        /// Get all doctor data.
        /// </summary>
        /// <returns>Collection of doctor data.</returns>
        public IEnumerable<DoctorViewModel> GetAll()
        {
            var doctorDomainModels = _doctorDal.GetAll();
            var doctorViewModels = new List<DoctorViewModel>();
            foreach (var doctorDomainModel in doctorDomainModels)
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
