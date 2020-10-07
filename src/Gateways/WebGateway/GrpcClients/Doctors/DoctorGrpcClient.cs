using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Doctors;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors
{
    public class DoctorGrpcClient : IDoctorGrpcClient
    {
        private readonly DoctorOptions _doctorOptions;
        private readonly Doctor.DoctorClient _client;

        public DoctorGrpcClient(IOptionsMonitor<DoctorOptions> optionsAccessor)
        {
            _doctorOptions = optionsAccessor.CurrentValue;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            GrpcChannel channel = GrpcChannel.ForAddress(_doctorOptions.DoctorApiUrl);
            _client = new Doctor.DoctorClient(channel);
        }

        async Task<DoctorModelsMessage> IDoctorGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }

        async Task<DoctorModelMessage> IDoctorGrpcClient.GetByIdAsync(IdMessage request)
        {
            return await _client.GetByIdAsync(request);
        }

        async Task IDoctorGrpcClient.AddAsync(DoctorModelMessage request)
        {
            await _client.AddAsync(request);
        }

        async Task IDoctorGrpcClient.UpdateAsync(UpdateMessage request)
        {
            await _client.UpdateAsync(request);
        }

        async Task IDoctorGrpcClient.DeleteAsync(IdMessage request)
        {
            await _client.DeleteAsync(request);
        }
    }
}
