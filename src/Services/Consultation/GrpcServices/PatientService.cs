using Grpc.Core;
using MedicalSystem.Services.Consultation.Protos;
using MedicalSystem.Services.Consultation.Queries;
using MedicalSystem.Services.Consultation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.GrpcServices
{
    /// <include file='docs.xml' path='docs/members[@name="PatientGrpcService"]/patientGrpcService/*'/>
    public class PatientService : Patient.PatientBase
    {
        private readonly IPatientQueries _patientQueries;

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcService"]/patientGrpcServiceConstructor/*'/>
        public PatientService(IPatientQueries patientQueries)
        {
            _patientQueries = patientQueries;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcService"]/getAll/*'/>
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