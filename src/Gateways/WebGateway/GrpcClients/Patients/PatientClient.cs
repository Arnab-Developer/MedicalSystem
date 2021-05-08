using MedicalSystem.Gateways.WebGateway.GrpcClients.Patients;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Protos.Patients
{
    public static partial class Patient
    {
        public partial class PatientClient : IPatientGrpcClient
        {
            async Task<PatientModelsMessage> IPatientGrpcClient.GetAllAsync(EmptyMessage request)
            {
                return await GetAllAsync(request);
            }

            async Task<PatientModelMessage> IPatientGrpcClient.GetByIdAsync(IdMessage request)
            {
                return await GetByIdAsync(request);
            }

            async Task IPatientGrpcClient.AddAsync(PatientModelMessage request)
            {
                await AddAsync(request);
            }

            async Task IPatientGrpcClient.UpdateAsync(UpdateMessage request)
            {
                await UpdateAsync(request);
            }

            async Task IPatientGrpcClient.DeleteAsync(IdMessage request)
            {
                await DeleteAsync(request);
            }
        }
    }
}

