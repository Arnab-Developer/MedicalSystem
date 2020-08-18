using Grpc.Core;
using MedicalSystem.Services.Consultation.Protos;
using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.GrpcServices
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorGrpcService"]/doctorGrpcService/*'/>
    public class DoctorService : Doctor.DoctorBase
    {
        private readonly IDoctorService _doctorService;

        /// <include file='docs.xml' path='docs/members[@name="DoctorGrpcService"]/doctorGrpcServiceConstructor/*'/>
        public DoctorService(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorGrpcService"]/getAll/*'/>
        public override Task<DoctorModelsMessage> GetAll(EmptyMessage request, ServerCallContext context)
        {
            IEnumerable<DoctorViewModel> doctorViewModels = _doctorService.GetAll();
            var doctorModelsMessage = new DoctorModelsMessage();
            foreach (DoctorViewModel doctor in doctorViewModels)
            {
                var doctorModelMessage = new DoctorModelMessage
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName
                };
                doctorModelsMessage.Doctors.Add(doctorModelMessage);
            }

            return Task.FromResult(doctorModelsMessage);
        }
    }
}
