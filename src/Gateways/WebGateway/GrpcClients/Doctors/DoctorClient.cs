using MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Protos.Doctors
{
    public static partial class Doctor
    {
        public partial class DoctorClient : IDoctorGrpcClient
        {
            async Task<DoctorModelsMessage> IDoctorGrpcClient.GetAllAsync(EmptyMessage request)
            {
                return await GetAllAsync(request);
            }

            async Task<DoctorModelMessage> IDoctorGrpcClient.GetByIdAsync(IdMessage request)
            {
                return await GetByIdAsync(request);
            }

            async Task IDoctorGrpcClient.AddAsync(DoctorModelMessage request)
            {
                await AddAsync(request);
            }

            async Task IDoctorGrpcClient.UpdateAsync(UpdateMessage request)
            {
                await UpdateAsync(request);
            }

            async Task IDoctorGrpcClient.DeleteAsync(IdMessage request)
            {
                await DeleteAsync(request);
            }
        }
    }
}
