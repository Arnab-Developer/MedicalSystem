using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Protos.Consultations
{
    public static partial class Doctor
    {
        public partial class DoctorClient : IDoctorGrpcClient
        {
            async Task<DoctorModelsMessage> IDoctorGrpcClient.GetAllAsync(EmptyMessage request)
            {
                return await GetAllAsync(request);
            }
        }
    }
}
