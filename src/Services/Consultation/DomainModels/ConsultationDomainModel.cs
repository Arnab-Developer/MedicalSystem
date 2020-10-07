using System;

namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/consultationDomainModel/*'/>
    public class ConsultationDomainModel
    {
        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/id/*'/>
        public int Id { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/date/*'/>
        public DateTime Date { get; set; }

        private Place _place;
        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/place/*'/>
        public Place? Place
        {
            get { return _place; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException();
                }
                _place = new Place(value.Country, value.State, value.City, value.PinCode);
            }
        }

        private string _problem;
        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/problem/*'/>
        public string? Problem
        {
            get { return _problem; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException();
                }
                _problem = value;
            }
        }

        private string _medicine;
        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/medicine/*'/>
        public string? Medicine
        {
            get { return _medicine; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                if (value.Length > 10)
                {
                    throw new ArgumentException();
                }
                _medicine = value;
            }
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/doctorId/*'/>
        public int DoctorId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/doctor/*'/>
        public DoctorDomainModel? Doctor { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/patientId/*'/>
        public int PatientId { get; set; }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/patient/*'/>
        public PatientDomainModel? Patient { get; set; }

        private ConsultationDomainModel()
        {
            Id = 0;
            Date = DateTime.Now;
            _place = new Place();
            _problem = string.Empty;
            _medicine = string.Empty;
            DoctorId = 0;
            PatientId = 0;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDomainModel"]/consultationDomainModelConstructor/*'/>
        public ConsultationDomainModel(int id, DateTime date, string? country, string? state, string? city,
            string? pinCode, string? problem, string? medicine, int doctorId, int patientId)
            : this()
        {
            Id = id;
            Date = date;
            Place = new Place(country, state, city, pinCode);
            Problem = problem;
            Medicine = medicine;
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}
