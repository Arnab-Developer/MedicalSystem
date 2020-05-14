using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <summary>
    /// Consultation service interface.
    /// </summary>
    public interface IConsultationService
    {
        /// <summary>
        /// Get all Consultation data.
        /// </summary>
        /// <returns>Collection of Consultation data.</returns>
        IEnumerable<ConsultationViewModel> GetAll();

        /// <summary>
        /// Get single Consultation data by id.
        /// </summary>
        /// <param name="id">Id of Consultation.</param>
        /// <returns>Single Consultation data.</returns>
        ConsultationViewModel? GetById(int id);

        /// <summary>
        /// Add new Consultation data.
        /// </summary>
        /// <param name="consultation">New Consultation data.</param>
        void Add(ConsultationViewModel consultation);

        /// <summary>
        /// Update existing Consultation data.
        /// </summary>
        /// <param name="id">Id of Consultation.</param>
        /// <param name="consultation">Existing Consultation data.</param>
        void Update(int id, ConsultationViewModel consultation);

        /// <summary>
        /// Delete Consultation data.
        /// </summary>
        /// <param name="id">Id of Consultation.</param>
        void Delete(int id);
    }
}
