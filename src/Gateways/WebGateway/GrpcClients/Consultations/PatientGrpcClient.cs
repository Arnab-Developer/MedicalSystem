using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    public class PatientGrpcClient : IPatientGrpcClient
    {
        private readonly IOptionsMonitor<ConsultationOptions> _optionsAccessor;
        private readonly Patient.PatientClient _client;

        public PatientGrpcClient(IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
            GrpcChannel channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.ConsultationApiUrl);
            _client = new Patient.PatientClient(channel);
        }

        async Task<PatientModelsMessage> IPatientGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }
    }
}
