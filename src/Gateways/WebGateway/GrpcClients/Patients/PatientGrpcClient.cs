using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Patients;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Patients
{
    /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/patientGrpcClient/*'/>
    public class PatientGrpcClient : IPatientGrpcClient
    {
        private readonly PatientOptions _patientOptions;
        private readonly Patient.PatientClient _client;

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/patientGrpcClientConstructor/*'/>
        public PatientGrpcClient(IOptionsMonitor<PatientOptions> optionsAccessor)
        {
            _patientOptions = optionsAccessor.CurrentValue;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            GrpcChannel channel = GrpcChannel.ForAddress(_patientOptions.PatientApiUrl);
            _client = new Patient.PatientClient(channel);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/getAllAsync/*'/>
        async Task<PatientModelsMessage> IPatientGrpcClient.GetAllAsync(EmptyMessage request)
        {
            return await _client.GetAllAsync(request);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/getByIdAsync/*'/>
        async Task<PatientModelMessage> IPatientGrpcClient.GetByIdAsync(IdMessage request)
        {
            return await _client.GetByIdAsync(request);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/addAsync/*'/>
        async Task IPatientGrpcClient.AddAsync(PatientModelMessage request)
        {
            await _client.AddAsync(request);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/updateAsync/*'/>
        async Task IPatientGrpcClient.UpdateAsync(UpdateMessage request)
        {
            await _client.UpdateAsync(request);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientGrpcClient"]/deleteAsync/*'/>
        async Task IPatientGrpcClient.DeleteAsync(IdMessage request)
        {
            await _client.DeleteAsync(request);
        }
    }
}
