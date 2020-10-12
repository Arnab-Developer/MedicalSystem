using System;

namespace MedicalSystem.Services.Consultation.Api.ViewModels
{
    public class ConsultationViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? PinCode { get; set; }

        public string? Problem { get; set; }

        public string? Medicine { get; set; }

        public int DoctorId { get; set; }

        public DoctorViewModel? Doctor { get; set; }

        public int PatientId { get; set; }

        public PatientViewModel? Patient { get; set; }
    }
}
