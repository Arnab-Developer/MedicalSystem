using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Protos.Consultations
{
    public static partial class Patient
    {
        public partial class PatientClient : IPatientGrpcClient
        {
            async Task<PatientModelsMessage> IPatientGrpcClient.GetAllAsync(EmptyMessage request)
            {
                return await GetAllAsync(request);
            }
        }
    }
}
