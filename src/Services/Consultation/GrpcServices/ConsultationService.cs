using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MedicalSystem.Services.Consultation.Protos;
using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.GrpcServices
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcService"]/consultationGrpcService/*'/>
    public class ConsultationService : Protos.Consultation.ConsultationBase
    {
        private readonly IConsultationService _consultationService;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcService"]/consultationGrpcServiceConstructor/*'/>
        public ConsultationService(IConsultationService consultationService)
        {
            _consultationService = consultationService;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcService"]/getAll/*'/>
        public override Task<ConsultationModelsMessage> GetAll(EmptyMessage request, ServerCallContext context)
        {
            IEnumerable<ConsultationViewModel> consultationViewModels = _consultationService.GetAll();
            var consultationModelsMessage = new ConsultationModelsMessage();
            foreach (ConsultationViewModel consultation in consultationViewModels)
            {
                var consultationModelMessage = new ConsultationModelMessage
                {
                    Id = consultation.Id,
                    Date = consultation.Date.ToUniversalTime().ToTimestamp(),
                    Country = consultation.Country,
                    State = consultation.State,
                    City = consultation.City,
                    PinCode = consultation.PinCode,
                    Problem = consultation.Problem,
                    Medicine = consultation.Medicine,
                    DoctorId = consultation.DoctorId,
                    Doctor = new DoctorModelMessage
                    {
                        Id = consultation.Doctor!.Id,
                        FirstName = consultation.Doctor!.FirstName,
                        LastName = consultation.Doctor!.LastName
                    },
                    PatientId = consultation.PatientId,
                    Patient = new PatientModelMessage
                    {
                        Id = consultation.Patient!.Id,
                        FirstName = consultation.Patient!.FirstName,
                        LastName = consultation.Patient!.LastName
                    }
                };
                consultationModelsMessage.Consultations.Add(consultationModelMessage);
            }

            return Task.FromResult(consultationModelsMessage);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcService"]/getById/*'/>
        public override Task<ConsultationModelMessage>? GetById(IdMessage request, ServerCallContext context)
        {
            ConsultationViewModel? consultationViewModel = _consultationService.GetById(request.Id);
            if (consultationViewModel == null)
            {
                return null;
            }
            var consultationModelMessage = new ConsultationModelMessage
            {
                Id = consultationViewModel.Id,
                Date = consultationViewModel.Date.ToUniversalTime().ToTimestamp(),
                Country = consultationViewModel.Country,
                State = consultationViewModel.State,
                City = consultationViewModel.City,
                PinCode = consultationViewModel.PinCode,
                Problem = consultationViewModel.Problem,
                Medicine = consultationViewModel.Medicine,
                DoctorId = consultationViewModel.DoctorId,
                Doctor = new DoctorModelMessage
                {
                    Id = consultationViewModel.Doctor!.Id,
                    FirstName = consultationViewModel.Doctor!.FirstName,
                    LastName = consultationViewModel.Doctor!.LastName
                },
                PatientId = consultationViewModel.PatientId,
                Patient = new PatientModelMessage
                {
                    Id = consultationViewModel.Patient!.Id,
                    FirstName = consultationViewModel.Patient!.FirstName,
                    LastName = consultationViewModel.Patient!.LastName
                }
            };
            return Task.FromResult(consultationModelMessage);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcService"]/add/*'/>
        public override Task<EmptyMessage> Add(ConsultationModelMessage request, ServerCallContext context)
        {
            var consultationViewModel = new ConsultationViewModel
            {
                Id = request.Id,
                Date = request.Date.ToDateTime(),
                Country = request.Country,
                State = request.State,
                City = request.City,
                PinCode = request.PinCode,
                Problem = request.Problem,
                Medicine = request.Medicine,
                DoctorId = request.DoctorId,
                PatientId = request.PatientId
            };
            _consultationService.Add(consultationViewModel);
            return Task.FromResult(new EmptyMessage());
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcService"]/update/*'/>
        public override Task<EmptyMessage> Update(UpdateMessage request, ServerCallContext context)
        {
            var consultationViewModel = new ConsultationViewModel
            {
                Id = request.Id,
                Date = request.Consultation.Date.ToDateTime(),
                Country = request.Consultation.Country,
                State = request.Consultation.State,
                City = request.Consultation.City,
                PinCode = request.Consultation.PinCode,
                Problem = request.Consultation.Problem,
                Medicine = request.Consultation.Medicine,
                DoctorId = request.Consultation.DoctorId,
                PatientId = request.Consultation.PatientId
            };
            _consultationService.Update(request.Id, consultationViewModel);
            return Task.FromResult(new EmptyMessage());
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcService"]/delete/*'/>
        public override Task<EmptyMessage> Delete(IdMessage request, ServerCallContext context)
        {
            _consultationService.Delete(request.Id);
            return Task.FromResult(new EmptyMessage());
        }
    }
}
