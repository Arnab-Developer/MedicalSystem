using MedicalSystem.Services.Consultation.Domain.SeedWork;
using System;

namespace MedicalSystem.Services.Consultation.Domain
{
    public class Place : ValueObject
    {
        private string _country;
        public string? Country
        {
            get { return _country; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                _country = value!;
            }
        }

        private string _state;
        public string? State
        {
            get { return _state; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                _state = value!;
            }
        }

        private string _city;
        public string? City
        {
            get { return _city; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                _city = value!;
            }
        }

        private string _pinCode;
        public string? PinCode
        {
            get { return _pinCode; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                _pinCode = value!;
            }
        }

        public Place()
        {
            _country = string.Empty;
            _state = string.Empty;
            _city = string.Empty;
            _pinCode = string.Empty;
        }

        public Place(string? country, string? state, string? city, string? pinCode)
            : this()
        {
            Country = country;
            State = state;
            City = city;
            PinCode = pinCode;
        }
    }
}
