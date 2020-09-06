using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/patientGrpcClient/*'/>
    public class PatientGrpcClient : IPatientGrpcClient
    {
        private readonly ConsultationOptions _consultationOptions;
        private readonly Patient.PatientClient _client;

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/patientGrpcClientConstructor/*'/>
        public PatientGrpcClient(IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _consultationOptions = optionsAccessor.CurrentValue;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            GrpcChannel channel = GrpcChannel.ForAddress(_consultationOptions.ConsultationApiUrl);
            _client = new Patient.PatientClient(channel);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/getAllAsync/*'/>
        async Task<PatientModelsMessage> IPatientGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }
    }
}
