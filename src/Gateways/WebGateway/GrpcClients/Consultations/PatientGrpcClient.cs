using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    public class PatientGrpcClient : IPatientGrpcClient, IDisposable
    {
        private readonly IOptionsMonitor<ConsultationOptions> _optionsAccessor;
        private readonly Patient.PatientClient _client;
        private readonly GrpcChannel _channel;
        private bool _disposedValue;

        public PatientGrpcClient(IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
            _channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.ConsultationApiUrl);
            _client = new Patient.PatientClient(_channel);
        }

        async Task<PatientModelsMessage> IPatientGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
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
        // ~PatientGrpcClient()
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
