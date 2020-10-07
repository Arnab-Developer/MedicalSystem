using Grpc.Core;
using MedicalSystem.Services.Consultation.Protos;
using MedicalSystem.Services.Consultation.Queries;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.GrpcServices
{
    public class PatientService : Patient.PatientBase
    {
        private readonly IPatientQueries _patientQueries;

        public PatientService(IPatientQueries patientQueries)
        {
            _patientQueries = patientQueries;
        }

        public override Task<PatientModelsMessage> GetAll(EmptyMessage request, ServerCallContext context)
        {
            IEnumerable<PatientViewModel> patientViewModels = _patientQueries.GetAll();
            var patientModelsMessage = new PatientModelsMessage();
            foreach (PatientViewModel patient in patientViewModels)
            {
                var patientModelMessage = new PatientModelMessage
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName
                };
                patientModelsMessage.Patients.Add(patientModelMessage);
            }

            return Task.FromResult(patientModelsMessage);
        }
    }
}