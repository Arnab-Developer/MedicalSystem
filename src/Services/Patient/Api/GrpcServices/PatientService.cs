using Grpc.Core;
using MedicalSystem.Services.Patient.Api.Data;
using MedicalSystem.Services.Patient.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Patient.Api.GrpcServices
{
    public class PatientService : Protos.Patient.PatientBase
    {
        private readonly PatientContext _patientContext;

        public PatientService(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }

        public override Task<Protos.PatientModelsMessage> GetAll(Protos.EmptyMessage request, ServerCallContext context)
        {
            IOrderedQueryable<PatientModel> patients = _patientContext.Patients!.OrderBy(patient => patient.FirstName);
            var patientModelsMessage = new Protos.PatientModelsMessage();
            foreach (PatientModel patient in patients)
            {
                var patientModelMessage = new Protos.PatientModelMessage
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName
                };
                patientModelsMessage.Patients.Add(patientModelMessage);
            }

            return Task.FromResult(patientModelsMessage);
        }

        public override Task<Protos.PatientModelMessage> GetById(Protos.IdMessage request, ServerCallContext context)
        {
            PatientModel? patient = _patientContext!.Patients!.FirstOrDefault(patient => patient.Id == request.Id);
            if (patient == null)
            {
                var emptyPatientModelMessage = new Protos.PatientModelMessage();
                return Task.FromResult(emptyPatientModelMessage);
            }
            var patientModelMessage = new Protos.PatientModelMessage
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };
            return Task.FromResult(patientModelMessage);
        }

        public override Task<Protos.EmptyMessage> Add(Protos.PatientModelMessage request, ServerCallContext context)
        {
            var patient = new PatientModel
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            _patientContext.Patients!.Add(patient);
            _patientContext.SaveChanges();

            return Task.FromResult(new Protos.EmptyMessage());
        }

        public override Task<Protos.EmptyMessage> Update(Protos.UpdateMessage request, ServerCallContext context)
        {
            PatientModel? d = _patientContext!.Patients!.FirstOrDefault(patient => patient.Id == request.Id);

            if (d != null)
            {
                d.FirstName = request.Patient.FirstName;
                d.LastName = request.Patient.LastName;

                _patientContext.SaveChanges();
            }

            return Task.FromResult(new Protos.EmptyMessage());
        }

        public override Task<Protos.EmptyMessage> Delete(Protos.IdMessage request, ServerCallContext context)
        {
            PatientModel? patient = _patientContext!.Patients!.FirstOrDefault(patient => patient.Id == request.Id);
            if (patient != null)
            {
                _patientContext.Patients!.Remove(patient);
                _patientContext.SaveChanges();
            }

            return Task.FromResult(new Protos.EmptyMessage());
        }
    }
}