using System;

namespace MedicalSystem.Services.Consultation.DomainModels
{
    public class ConsultationDomainModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        private string _place;
        public string? Place
        {
            get { return _place; }
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
                _place = value;
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
                if (value.Length > 10)
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
                if (value.Length > 10)
                {
                    throw new ArgumentException();
                }
                _medicine = value;
            }
        }

        public int DoctorId { get; set; }
        public DoctorDomainModel? Doctor { get; set; }
        public int PatentId { get; set; }
        public PatentDomainModel? Patent { get; set; }

        public ConsultationDomainModel()
        {
            Id = 0;
            Date = DateTime.Now;
            _place = string.Empty;
            _problem = string.Empty;
            _medicine = string.Empty;
            DoctorId = 0;
            PatentId = 0;
        }

        public ConsultationDomainModel(int id, DateTime date, string? place, string? problem,
            string? medicine, int doctorId, int patentId)
            : this()
        {
            Id = id;
            Date = date;
            Place = place;
            Problem = problem;
            Medicine = medicine;
            DoctorId = doctorId;
            PatentId = patentId;
        }
    }
}
