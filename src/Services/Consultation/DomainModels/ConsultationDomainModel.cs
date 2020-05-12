using System;

namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <summary>
    /// Consultation model.
    /// </summary>
    internal class ConsultationDomainModel
    {
        /// <summary>
        /// Id of Consultation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date of Consultation.
        /// </summary>
        public DateTime Date { get; set; }

        private Place _place;
        /// <summary>
        /// Place of Consultation.
        /// </summary>
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
        /// <summary>
        /// Problem of Consultation.
        /// </summary>
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
        /// <summary>
        /// Medicine of Consultation.
        /// </summary>
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

        /// <summary>
        /// Id of doctor.
        /// </summary>
        public int DoctorId { get; set; }
        
        /// <summary>
        /// Doctor navigation property.
        /// </summary>
        public DoctorDomainModel? Doctor { get; set; }
        
        /// <summary>
        /// Id of patent.
        /// </summary>
        public int PatentId { get; set; }
        
        /// <summary>
        /// Patent navigation property.
        /// </summary>
        public PatentDomainModel? Patent { get; set; }

        private ConsultationDomainModel()
        {
            Id = 0;
            Date = DateTime.Now;
            _place = new Place();
            _problem = string.Empty;
            _medicine = string.Empty;
            DoctorId = 0;
            PatentId = 0;
        }

        /// <summary>
        /// Create new Consultation object.
        /// </summary>
        /// <param name="id">Id of Consultation.</param>
        /// <param name="date">Date of Consultation.</param>
        /// <param name="country">Country of Consultation.</param>
        /// <param name="state">State of Consultation.</param>
        /// <param name="city">City of Consultation.</param>
        /// <param name="pinCode">Pincode of Consultation.</param>
        /// <param name="problem">Problem of Consultation.</param>
        /// <param name="medicine">Medicine of Consultation.</param>
        /// <param name="doctorId">Id of doctor for Consultation.</param>
        /// <param name="patentId">Id of patent for Consultation.</param>
        public ConsultationDomainModel(int id, DateTime date, string? country, string? state, string? city,
            string? pinCode, string? problem, string? medicine, int doctorId, int patentId)
            : this()
        {
            Id = id;
            Date = date;
            Place = new Place(country, state, city, pinCode);
            Problem = problem;
            Medicine = medicine;
            DoctorId = doctorId;
            PatentId = patentId;
        }
    }
}
