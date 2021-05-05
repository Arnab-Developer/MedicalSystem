using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Doctors;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors
{
    public class DoctorGrpcClient : IDoctorGrpcClient, IDisposable
    {
        private readonly IOptionsMonitor<DoctorOptions> _optionsAccessor;
        private readonly Doctor.DoctorClient _client;
        private readonly GrpcChannel _channel;
        private bool _disposedValue;

        public DoctorGrpcClient(IOptionsMonitor<DoctorOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
            _channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.DoctorApiUrl);
            _client = new Doctor.DoctorClient(_channel);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _channel.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DoctorGrpcClient()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
