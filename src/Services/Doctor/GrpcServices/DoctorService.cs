using Grpc.Core;
using MedicalSystem.Services.Doctor.Data;
using MedicalSystem.Services.Doctor.Models;
using System.Linq;
using System.Threading.Tasks;
using Protos = MedicalSystem.Services.Doctor.Protos;

namespace MedicalSystem.Services.Doctor.GrpcServices
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorController/*'/>
    public class DoctorService : Protos::Doctor.DoctorBase
    {
        private readonly DoctorContext _doctorContext;

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorControllerConstructor/*'/>
        public DoctorService(DoctorContext doctorContext)
        {
            _doctorContext = doctorContext;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getAll/*'/>
        public override Task<Protos::DoctorModelsMessage> GetAll(Protos::EmptyMessage request, ServerCallContext context)
        {
            IOrderedQueryable<DoctorModel> doctors = _doctorContext.Doctors.OrderBy(doctor => doctor.FirstName);
            var doctorModelsMessage = new Protos::DoctorModelsMessage();
            foreach (DoctorModel doctor in doctors)
            {
                var doctorModelMessage = new Protos::DoctorModelMessage
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName
                };
                doctorModelsMessage.Doctors.Add(doctorModelMessage);
            }

            return Task.FromResult(doctorModelsMessage);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getById/*'/>
        public override Task<Protos::DoctorModelMessage> GetById(Protos::IdMessage request, ServerCallContext context)
        {
            DoctorModel doctor = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == request.Id);
            if (doctor == null)
            {
                var emptyDoctorModelMessage = new Protos::DoctorModelMessage();
                return Task.FromResult(emptyDoctorModelMessage);
            }
            var doctorModelMessage = new Protos::DoctorModelMessage
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };
            return Task.FromResult(doctorModelMessage);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/add/*'/>
        public override Task<Protos::EmptyMessage> Add(Protos::DoctorModelMessage request, ServerCallContext context)
        {
            var doctor = new DoctorModel
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            _doctorContext.Doctors!.Add(doctor);
            _doctorContext.SaveChanges();

            return Task.FromResult(new Protos::EmptyMessage());
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/update/*'/>
        public override Task<Protos::EmptyMessage> Update(Protos::UpdateMessage request, ServerCallContext context)
        {
            DoctorModel d = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == request.Id);

            if (d != null)
            {
                d.FirstName = request.Doctor.FirstName;
                d.LastName = request.Doctor.LastName;

                _doctorContext.SaveChanges();
            }

            return Task.FromResult(new Protos::EmptyMessage());
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/delete/*'/>
        public override Task<Protos::EmptyMessage> Delete(Protos::IdMessage request, ServerCallContext context)
        {
            DoctorModel doctor = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == request.Id);
            if (doctor != null)
            {
                _doctorContext.Doctors!.Remove(doctor);
                _doctorContext.SaveChanges();
            }

            return Task.FromResult(new Protos::EmptyMessage());
        }
    }
}
