using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <summary>
    /// Doctor service interface.
    /// </summary>
    public interface IDoctorService
    {
        /// <summary>
        /// Get all doctor data.
        /// </summary>
        /// <returns>Collection of doctor data.</returns>
        IEnumerable<DoctorViewModel> GetAll();
    }
}
