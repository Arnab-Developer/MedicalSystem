using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorGrpcClient"]/doctorGrpcClient/*'/>
    public class DoctorGrpcClient : IDoctorGrpcClient
    {
        private readonly ConsultationOptions _consultationOptions;
        private readonly Doctor.DoctorClient _client;

        /// <include file='docs.xml' path='docs/members[@name="DoctorGrpcClient"]/doctorGrpcClientConstructor/*'/>
        public DoctorGrpcClient(IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _consultationOptions = optionsAccessor.CurrentValue;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            GrpcChannel channel = GrpcChannel.ForAddress(_consultationOptions.ConsultationApiUrl);
            _client = new Doctor.DoctorClient(channel);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorGrpcClient"]/getAllAsync/*'/>
        async Task<DoctorModelsMessage> IDoctorGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }
    }
}
