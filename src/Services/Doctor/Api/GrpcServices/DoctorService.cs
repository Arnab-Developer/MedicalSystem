using Grpc.Core;
using MedicalSystem.Services.Doctor.Api.Data;
using MedicalSystem.Services.Doctor.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Doctor.Api.GrpcServices
{
    public class DoctorService : Protos.Doctor.DoctorBase
    {
        private readonly DoctorContext _doctorContext;

        public DoctorService(DoctorContext doctorContext)
        {
            _doctorContext = doctorContext;
        }

        public override Task<Protos.DoctorModelsMessage> GetAll(Protos.EmptyMessage request, ServerCallContext context)
        {
            IOrderedQueryable<DoctorModel>? doctors = _doctorContext.Doctors!.OrderBy(doctor => doctor.FirstName);
            var doctorModelsMessage = new Protos.DoctorModelsMessage();
            foreach (DoctorModel doctor in doctors)
            {
                var doctorModelMessage = new Protos.DoctorModelMessage
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName
                };
                doctorModelsMessage.Doctors.Add(doctorModelMessage);
            }

            return Task.FromResult(doctorModelsMessage);
        }

        public override Task<Protos.DoctorModelMessage> GetById(Protos.IdMessage request, ServerCallContext context)
        {
            DoctorModel? doctor = _doctorContext!.Doctors!.FirstOrDefault(doctor => doctor.Id == request.Id);
            if (doctor == null)
            {
                var emptyDoctorModelMessage = new Protos.DoctorModelMessage();
                return Task.FromResult(emptyDoctorModelMessage);
            }
            var doctorModelMessage = new Protos.DoctorModelMessage
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };
            return Task.FromResult(doctorModelMessage);
        }

        public override Task<Protos.EmptyMessage> Add(Protos.DoctorModelMessage request, ServerCallContext context)
        {
            var doctor = new DoctorModel
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            _doctorContext.Doctors!.Add(doctor);
            _doctorContext.SaveChanges();

            return Task.FromResult(new Protos.EmptyMessage());
        }

        public override Task<Protos.EmptyMessage> Update(Protos.UpdateMessage request, ServerCallContext context)
        {
            DoctorModel? d = _doctorContext!.Doctors!.FirstOrDefault(doctor => doctor.Id == request.Id);

            if (d != null)
            {
                d.FirstName = request.Doctor.FirstName;
                d.LastName = request.Doctor.LastName;

                _doctorContext.SaveChanges();
            }

            return Task.FromResult(new Protos.EmptyMessage());
        }

        public override Task<Protos.EmptyMessage> Delete(Protos.IdMessage request, ServerCallContext context)
        {
            DoctorModel? doctor = _doctorContext!.Doctors!.FirstOrDefault(doctor => doctor.Id == request.Id);
            if (doctor != null)
            {
                _doctorContext.Doctors!.Remove(doctor);
                _doctorContext.SaveChanges();
            }

            return Task.FromResult(new Protos.EmptyMessage());
        }
    }
}
