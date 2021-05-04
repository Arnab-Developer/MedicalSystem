using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    public class DoctorGrpcClient : IDoctorGrpcClient
    {
        private readonly IOptionsMonitor<ConsultationOptions> _optionsAccessor;
        private readonly Doctor.DoctorClient _client;

        public DoctorGrpcClient(IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
            GrpcChannel channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.ConsultationApiUrl);
            _client = new Doctor.DoctorClient(channel);
        }

        async Task<DoctorModelsMessage> IDoctorGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }
    }
}
