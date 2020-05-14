using System;

namespace MedicalSystem.Services.Consultation.ViewModels
{
    /// <summary>
    /// Consultation class.
    /// </summary>
    public class ConsultationViewModel
    {
        /// <summary>
        /// Id of Consultation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date of Consultation.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Country of Consultation.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// State of Consultation.
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// City of Consultation.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Pincode of Consultation.
        /// </summary>
        public string? PinCode { get; set; }

        /// <summary>
        /// Problem of Consultation.
        /// </summary>
        public string? Problem { get; set; }

        /// <summary>
        /// Medicine of Consultation.
        /// </summary>
        public string? Medicine { get; set; }

        /// <summary>
        /// Doctor id of Consultation.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Doctor of Consultation.
        /// </summary>
        public DoctorViewModel? Doctor { get; set; }

        /// <summary>
        /// Patent id of Consultation.
        /// </summary>
        public int PatentId { get; set; }

        /// <summary>
        /// Patent of Consultation.
        /// </summary>
        public PatentViewModel? Patent { get; set; }
    }
}
