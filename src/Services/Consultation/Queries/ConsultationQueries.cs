using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Queries
{
    public class ConsultationQueries : IConsultationQueries
    {
        private readonly ConsultationContext _consultationContext;

        public ConsultationQueries(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        IEnumerable<ConsultationViewModel> IConsultationQueries.GetAll()
        {
            IOrderedQueryable<ConsultationDomainModel> consultationDomainModels = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patient)
                .OrderByDescending(consultation => consultation.Date);
            var consultationViewModels = new List<ConsultationViewModel>();
            foreach (ConsultationDomainModel consultationDomainModel in consultationDomainModels)
            {
                var consultationViewModel = new ConsultationViewModel()
                {
                    Id = consultationDomainModel.Id,
                    Date = consultationDomainModel.Date,
                    Country = consultationDomainModel.Place!.Country,
                    State = consultationDomainModel.Place!.State,
                    City = consultationDomainModel.Place!.City,
                    PinCode = consultationDomainModel.Place!.PinCode,
                    Problem = consultationDomainModel.Problem,
                    Medicine = consultationDomainModel.Medicine,
                    DoctorId = consultationDomainModel.DoctorId,
                    Doctor = new DoctorViewModel()
                    {
                        Id = consultationDomainModel.Doctor!.Id,
                        FirstName = consultationDomainModel.Doctor.FirstName,
                        LastName = consultationDomainModel.Doctor.LastName
                    },
                    PatientId = consultationDomainModel.PatientId,
                    Patient = new PatientViewModel()
                    {
                        Id = consultationDomainModel.Patient!.Id,
                        FirstName = consultationDomainModel.Patient.FirstName,
                        LastName = consultationDomainModel.Patient.LastName
                    }
                };
                consultationViewModels.Add(consultationViewModel);
            }
            return consultationViewModels;
        }

        ConsultationViewModel? IConsultationQueries.GetById(int id)
        {
            ConsultationDomainModel consultationDomainModel = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patient)
                .FirstOrDefault(consultation => consultation.Id == id);
            if (consultationDomainModel == null)
            {
                return null;
            }
            var consultationViewModel = new ConsultationViewModel()
            {
                Id = consultationDomainModel.Id,
                Date = consultationDomainModel.Date,
                Country = consultationDomainModel.Place!.Country,
                State = consultationDomainModel.Place!.State,
                City = consultationDomainModel.Place!.City,
                PinCode = consultationDomainModel.Place!.PinCode,
                Problem = consultationDomainModel.Problem,
                Medicine = consultationDomainModel.Medicine,
                DoctorId = consultationDomainModel.DoctorId,
                Doctor = new DoctorViewModel()
                {
                    Id = consultationDomainModel.Doctor!.Id,
                    FirstName = consultationDomainModel.Doctor.FirstName,
                    LastName = consultationDomainModel.Doctor.LastName
                },
                PatientId = consultationDomainModel.PatientId,
                Patient = new PatientViewModel()
                {
                    Id = consultationDomainModel.Patient!.Id,
                    FirstName = consultationDomainModel.Patient.FirstName,
                    LastName = consultationDomainModel.Patient.LastName
                }
            };
            return consultationViewModel;
        }
    }
}
