using MedicalSystem.Services.Consultation.Data;
using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly ConsultationContext _consultationContext;

        public ConsultationService(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        public IEnumerable<ConsultationViewModel> GetAll()
        {
            var consultationDomainModels = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patent)
                .OrderByDescending(consultation => consultation.Date);

            var consultationViewModels = new List<ConsultationViewModel>();
            foreach (var consultationDomainModel in consultationDomainModels)
            {
                var consultationViewModel = new ConsultationViewModel()
                {
                    Id = consultationDomainModel.Id,
                    Date = consultationDomainModel.Date,
                    Place = consultationDomainModel.Place,
                    Problem = consultationDomainModel.Problem,
                    Medicine = consultationDomainModel.Medicine,
                    DoctorId = consultationDomainModel.DoctorId,
                    Doctor = new DoctorViewModel()
                    {
                        Id = consultationDomainModel.Doctor!.Id,
                        FirstName = consultationDomainModel.Doctor.FirstName,
                        LastName = consultationDomainModel.Doctor.LastName
                    },
                    Patent = new PatentViewModel()
                    {
                        Id = consultationDomainModel.Patent!.Id,
                        FirstName = consultationDomainModel.Patent.FirstName,
                        LastName = consultationDomainModel.Patent.LastName
                    }
                };
                consultationViewModels.Add(consultationViewModel);
            }

            return consultationViewModels;
        }

        public ConsultationViewModel? GetById(int id)
        {
            var consultationDomainModel = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patent)
                .FirstOrDefault(consultation => consultation.Id == id);
            
            if(consultationDomainModel == null)
            {                
                return null;
            }
            
            var consultationViewModel = new ConsultationViewModel()
            {
                Id = consultationDomainModel.Id,
                Date = consultationDomainModel.Date,
                Place = consultationDomainModel.Place,
                Problem = consultationDomainModel.Problem,
                Medicine = consultationDomainModel.Medicine,
                DoctorId = consultationDomainModel.DoctorId,
                Doctor = new DoctorViewModel()
                {
                    Id = consultationDomainModel.Doctor!.Id,
                    FirstName = consultationDomainModel.Doctor.FirstName,
                    LastName = consultationDomainModel.Doctor.LastName
                },
                Patent = new PatentViewModel()
                {
                    Id = consultationDomainModel.Patent!.Id,
                    FirstName = consultationDomainModel.Patent.FirstName,
                    LastName = consultationDomainModel.Patent.LastName
                }
            };
            return consultationViewModel;
        }

        public void Add(ConsultationViewModel consultationViewModel)
        {
            var consultationDomainModel = new ConsultationDomainModel(consultationViewModel.Id,
                consultationViewModel.Date, consultationViewModel.Place, consultationViewModel.Problem,
                consultationViewModel.Medicine, consultationViewModel.DoctorId, consultationViewModel.PatentId);

            _consultationContext.Consultations.Add(consultationDomainModel);
            _consultationContext.SaveChanges();
        }

        public void Update(int id, ConsultationViewModel consultationViewModel)
        {
            var consultationDomainModel = _consultationContext.Consultations
                .FirstOrDefault(consultation => consultation.Id == id);

            consultationDomainModel.Date = consultationViewModel.Date;
            consultationDomainModel.Place = consultationViewModel.Place;
            consultationDomainModel.Problem = consultationViewModel.Problem;
            consultationDomainModel.Medicine = consultationViewModel.Medicine;
            consultationDomainModel.DoctorId = consultationViewModel.DoctorId;
            consultationDomainModel.PatentId = consultationViewModel.PatentId;

            _consultationContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var consultationDomainModel = _consultationContext.Consultations
                .FirstOrDefault(consultation => consultation.Id == id);
            _consultationContext.Consultations.Remove(consultationDomainModel);
            _consultationContext.SaveChanges();
        }
    }
}
