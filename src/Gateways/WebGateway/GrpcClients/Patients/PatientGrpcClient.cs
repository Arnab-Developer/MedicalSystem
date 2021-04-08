using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Patients;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Patients
{
    public class PatientGrpcClient : IPatientGrpcClient
    {
        private readonly IOptionsMonitor<PatientOptions> _optionsAccessor;
        private readonly Patient.PatientClient _client;

        public PatientGrpcClient(IOptionsMonitor<PatientOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            GrpcChannel channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.PatientApiUrl);
            _client = new Patient.PatientClient(channel);
        }

        async Task<PatientModelsMessage> IPatientGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }

        async Task<PatientModelMessage> IPatientGrpcClient.GetByIdAsync(IdMessage request)
        {
            return await _client.GetByIdAsync(request);
        }

        async Task IPatientGrpcClient.AddAsync(PatientModelMessage request)
        {
            await _client.AddAsync(request);
        }

        async Task IPatientGrpcClient.UpdateAsync(UpdateMessage request)
        {
            await _client.UpdateAsync(request);
        }

        async Task IPatientGrpcClient.DeleteAsync(IdMessage request)
        {
            await _client.DeleteAsync(request);
        }
    }
}
