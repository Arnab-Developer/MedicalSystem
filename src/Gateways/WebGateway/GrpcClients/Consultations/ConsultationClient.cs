using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Protos.Consultations
{
    public static partial class Consultation
    {
        public partial class ConsultationClient : IConsultationGrpcClient
        {
            async Task<ConsultationModelsMessage> IConsultationGrpcClient.GetAllAsync(EmptyMessage request)
            {
                return await GetAllAsync(request);
            }

            async Task<ConsultationModelMessage> IConsultationGrpcClient.GetByIdAsync(IdMessage request)
            {
                return await GetByIdAsync(request);
            }

            async Task IConsultationGrpcClient.AddAsync(ConsultationModelMessage request)
            {
                await AddAsync(request);
            }

            async Task IConsultationGrpcClient.UpdateAsync(UpdateMessage request)
            {
                await UpdateAsync(request);
            }

            async Task IConsultationGrpcClient.DeleteAsync(IdMessage request)
            {
                await DeleteAsync(request);
            }
        }
    }
}
