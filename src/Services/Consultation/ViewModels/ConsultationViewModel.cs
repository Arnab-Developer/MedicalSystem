using System;

namespace MedicalSystem.Services.Consultation.ViewModels
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/consultationViewModel/*'/>
    public class ConsultationViewModel
    {
        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/id/*'/>
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/date/*'/>
        public DateTime Date { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/country/*'/>
        public string? Country { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/state/*'/>
        public string? State { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/city/*'/>
        public string? City { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/pinCode/*'/>
        public string? PinCode { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/problem/*'/>
        public string? Problem { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/medicine/*'/>
        public string? Medicine { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/doctorId/*'/>
        public int DoctorId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/doctor/*'/>
        public DoctorViewModel? Doctor { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/patientId/*'/>
        public int PatientId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationViewModel"]/patient/*'/>
        public PatientViewModel? Patient { get; set; }
    }
}
