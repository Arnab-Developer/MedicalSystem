using System;

namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <summary>
    /// Place class. This is a value object.
    /// </summary>
    internal class Place
    {
        private string _country;
        /// <summary>
        /// Country of place.
        /// </summary>
        public string? Country
        {
            get { return _country; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                _country = value;
            }
        }

        private string _state;
        /// <summary>
        /// State of place.
        /// </summary>
        public string? State
        {
            get { return _state; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                _state = value;
            }
        }

        private string _city;
        /// <summary>
        /// City of place.
        /// </summary>
        public string? City
        {
            get { return _city; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                _city = value;
            }
        }

        private string _pinCode;
        /// <summary>
        /// Pincode of place.
        /// </summary>
        public string? PinCode
        {
            get { return _pinCode; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                _pinCode = value;
            }
        }

        /// <summary>
        /// Create new place object with default value.
        /// </summary>
        public Place()
        {
            _country = string.Empty;
            _state = string.Empty;
            _city = string.Empty;
            _pinCode = string.Empty;
        }

        /// <summary>
        /// Create new place object with input value.
        /// </summary>
        /// <param name="country">Country of place.</param>
        /// <param name="state">State of place.</param>
        /// <param name="city">City of place.</param>
        /// <param name="pinCode">Pincode of place.</param>
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
