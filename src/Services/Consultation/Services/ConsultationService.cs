using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Services
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationService"]/consultationService/*'/>
    internal class ConsultationService : IConsultationService
    {
        private readonly IConsultationDal _consultationDal;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationService"]/consultationServiceConstructor/*'/>
        public ConsultationService(IConsultationDal consultationDal)
        {
            _consultationDal = consultationDal;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationService"]/getAll/*'/>
        public IEnumerable<ConsultationViewModel> GetAll()
        {
            var consultationDomainModels = _consultationDal.GetAll();

            var consultationViewModels = new List<ConsultationViewModel>();
            foreach (var consultationDomainModel in consultationDomainModels)
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
                    PatentId = consultationDomainModel.PatentId,
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

        /// <include file='docs.xml' path='docs/members[@name="ConsultationService"]/getById/*'/>
        public ConsultationViewModel? GetById(int id)
        {
            var consultationDomainModel = _consultationDal.GetById(id);

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
                PatentId = consultationDomainModel.PatentId,
                Patent = new PatentViewModel()
                {
                    Id = consultationDomainModel.Patent!.Id,
                    FirstName = consultationDomainModel.Patent.FirstName,
                    LastName = consultationDomainModel.Patent.LastName
                }
            };
            return consultationViewModel;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationService"]/add/*'/>
        public void Add(ConsultationViewModel consultationViewModel)
        {
            var consultationDomainModel = new ConsultationDomainModel(consultationViewModel.Id,
                consultationViewModel.Date, consultationViewModel.Country, consultationViewModel.State,
                consultationViewModel.City, consultationViewModel.PinCode, consultationViewModel.Problem,
                consultationViewModel.Medicine, consultationViewModel.DoctorId, consultationViewModel.PatentId);

            _consultationDal.Add(consultationDomainModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationService"]/update/*'/>
        public void Update(int id, ConsultationViewModel consultationViewModel)
        {
            var consultationDomainModel = _consultationDal.GetById(id);

            if (consultationDomainModel == null)
            {
                return;
            }

            consultationDomainModel.Date = consultationViewModel.Date;
            consultationDomainModel.Place = new Place(consultationViewModel.Country,
                consultationViewModel.State, consultationViewModel.City, consultationViewModel.PinCode);
            consultationDomainModel.Problem = consultationViewModel.Problem;
            consultationDomainModel.Medicine = consultationViewModel.Medicine;
            consultationDomainModel.DoctorId = consultationViewModel.DoctorId;
            consultationDomainModel.PatentId = consultationViewModel.PatentId;

            _consultationDal.Update(consultationDomainModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationService"]/delete/*'/>
        public void Delete(int id)
        {
            var consultationDomainModel = _consultationDal.GetById(id);
            if (consultationDomainModel == null)
            {
                return;
            }
            _consultationDal.Delete(consultationDomainModel);
        }
    }
}
