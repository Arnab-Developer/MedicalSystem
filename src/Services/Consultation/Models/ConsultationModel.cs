using System;

namespace MedicalSystem.Services.Consultation.Models
{
    public class ConsultationModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Problem { get; set; }
        public string Medicine { get; set; }
        public int DoctorId { get; set; }
        public DoctorModel Doctor { get; set; }
        public int PatentId { get; set; }
        public PatentModel Patent { get; set; }
    }
}
