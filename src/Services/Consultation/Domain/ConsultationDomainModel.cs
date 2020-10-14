using MedicalSystem.Services.Consultation.Domain.SeedWork;
using System;

namespace MedicalSystem.Services.Consultation.Domain
{
    public class ConsultationDomainModel : Entity, IAggregateRoot
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        private Place _place;
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
        public string? Problem
        {
            get { return _problem; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                if (value!.Length > 10)
                {
                    throw new ArgumentException();
                }
                _problem = value;
            }
        }

        private string _medicine;
        public string? Medicine
        {
            get { return _medicine; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                if (value!.Length > 10)
                {
                    throw new ArgumentException();
                }
                _medicine = value;
            }
        }

        public int DoctorId { get; set; }

        public DoctorDomainModel? Doctor { get; set; }

        public int PatientId { get; set; }

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
