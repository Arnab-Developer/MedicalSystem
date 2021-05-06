using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Protos.Consultations
{
    public static partial class Doctor
    {
        public partial class DoctorClient : IDoctorGrpcClient, IDisposable
        {
            private readonly GrpcChannel _channel;
            private bool _disposedValue;

            public DoctorClient(GrpcChannel channel, bool isDisposable)
                : this(channel)
            {
                _channel = channel;
            }

            async Task<DoctorModelsMessage> IDoctorGrpcClient.GetAllAsync(EmptyMessage request)
            {
                return await GetAllAsync(request);
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
            // ~DoctorClient()
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
}
