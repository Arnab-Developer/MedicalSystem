using System;

namespace MedicalSystem.Services.Consultation.ViewModels
{
    public class ConsultationViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Place { get; set; }
        public string? Problem { get; set; }
        public string? Medicine { get; set; }
        public int DoctorId { get; set; }
        public DoctorViewModel? Doctor { get; set; }
        public int PatentId { get; set; }
        public PatentViewModel? Patent { get; set; }
    }
}
