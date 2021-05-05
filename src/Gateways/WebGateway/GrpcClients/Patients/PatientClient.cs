using MedicalSystem.Gateways.WebGateway.GrpcClients.Patients;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Protos.Patients
{
    public static partial class Patient
    {
        public partial class PatientClient : IPatientGrpcClient
        {
            async Task<PatientModelsMessage> IPatientGrpcClient.GetAllAsync(EmptyMessage request)
            {
                return await GetAllAsync(request);
            }

            async Task<PatientModelMessage> IPatientGrpcClient.GetByIdAsync(IdMessage request)
            {
                return await GetByIdAsync(request);
            }

            async Task IPatientGrpcClient.AddAsync(PatientModelMessage request)
            {
                await AddAsync(request);
            }

            async Task IPatientGrpcClient.UpdateAsync(UpdateMessage request)
            {
                await UpdateAsync(request);
            }

            async Task IPatientGrpcClient.DeleteAsync(IdMessage request)
            {
                await DeleteAsync(request);
            }
        }
    }
}

/*namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Patients
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
}*/
