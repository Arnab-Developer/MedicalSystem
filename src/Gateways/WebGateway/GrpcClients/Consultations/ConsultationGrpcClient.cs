using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    public class ConsultationGrpcClient : IConsultationGrpcClient
    {
        private readonly IOptionsMonitor<ConsultationOptions> _optionsAccessor;
        private readonly Consultation.ConsultationClient _client;

        public ConsultationGrpcClient(IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
            GrpcChannel channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.ConsultationApiUrl);
            _client = new Consultation.ConsultationClient(channel);
        }

        async Task<ConsultationModelsMessage> IConsultationGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }

        async Task<ConsultationModelMessage> IConsultationGrpcClient.GetByIdAsync(IdMessage request)
        {
            return await _client.GetByIdAsync(request);
        }

        async Task IConsultationGrpcClient.AddAsync(ConsultationModelMessage request)
        {
            await _client.AddAsync(request);
        }

        async Task IConsultationGrpcClient.UpdateAsync(UpdateMessage request)
        {
            await _client.UpdateAsync(request);
        }

        async Task IConsultationGrpcClient.DeleteAsync(IdMessage request)
        {
            await _client.DeleteAsync(request);
        }
    }
}
