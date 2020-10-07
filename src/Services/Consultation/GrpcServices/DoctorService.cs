using Grpc.Core;
using MedicalSystem.Services.Consultation.Protos;
using MedicalSystem.Services.Consultation.Queries;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.GrpcServices
{
    public class DoctorService : Doctor.DoctorBase
    {
        private readonly IDoctorQueries _doctorQueries;

        public DoctorService(IDoctorQueries doctorQueries)
        {
            _doctorQueries = doctorQueries;
        }

        public override Task<DoctorModelsMessage> GetAll(EmptyMessage request, ServerCallContext context)
        {
            IEnumerable<DoctorViewModel> doctorViewModels = _doctorQueries.GetAll();
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
