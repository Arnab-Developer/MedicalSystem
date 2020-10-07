using System;

namespace MedicalSystem.Services.Consultation.DomainModels
{
    /// <include file='docs.xml' path='docs/members[@name="Place"]/place/*'/>
    public class Place
    {
        private string _country;
        /// <include file='docs.xml' path='docs/members[@name="Place"]/country/*'/>
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
        /// <include file='docs.xml' path='docs/members[@name="Place"]/state/*'/>
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
        /// <include file='docs.xml' path='docs/members[@name="Place"]/city/*'/>
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
        /// <include file='docs.xml' path='docs/members[@name="Place"]/pinCode/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="Place"]/placeDefaultConstructor/*'/>
        public Place()
        {
            _country = string.Empty;
            _state = string.Empty;
            _city = string.Empty;
            _pinCode = string.Empty;
        }

        /// <include file='docs.xml' path='docs/members[@name="Place"]/placeParameterConstructor/*'/>
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
