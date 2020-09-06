using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcClient"]/consultationGrpcClient/*'/>
    public class ConsultationGrpcClient : IConsultationGrpcClient
    {
        private readonly ConsultationOptions _consultationOptions;
        private readonly Consultation.ConsultationClient _client;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcClient"]/consultationGrpcClientConstructor/*'/>
        public ConsultationGrpcClient(IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _consultationOptions = optionsAccessor.CurrentValue;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            GrpcChannel channel = GrpcChannel.ForAddress(_consultationOptions.ConsultationApiUrl);
            _client = new Consultation.ConsultationClient(channel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcClient"]/getAllAsync/*'/>
        async Task<ConsultationModelsMessage> IConsultationGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcClient"]/getByIdAsync/*'/>
        async Task<ConsultationModelMessage> IConsultationGrpcClient.GetByIdAsync(IdMessage request)
        {
            return await _client.GetByIdAsync(request);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcClient"]/addAsync/*'/>
        async Task IConsultationGrpcClient.AddAsync(ConsultationModelMessage request)
        {
            await _client.AddAsync(request);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcClient"]/updateAsync/*'/>
        async Task IConsultationGrpcClient.UpdateAsync(UpdateMessage request)
        {
            await _client.UpdateAsync(request);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationGrpcClient"]/deleteAsync/*'/>
        async Task IConsultationGrpcClient.DeleteAsync(IdMessage request)
        {
            await _client.DeleteAsync(request);
        }
    }
}
