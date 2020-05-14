using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <summary>
    /// Patent service interface.
    /// </summary>
    public interface IPatentService
    {
        /// <summary>
        /// Get all patent data.
        /// </summary>
        /// <returns>Collection of patent data.</returns>
        IEnumerable<PatentViewModel> GetAll();
    }
}
