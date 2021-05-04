using Grpc.Net.Client;
using MedicalSystem.Admin.HealthCheckDashboard.Options;
using MedicalSystem.Admin.HealthCheckDashboard.Protos.Patients;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Admin.HealthCheckDashboard.ApiHealthChecks
{
    internal class PatientApiHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<PatientOptions> _optionsAccessor;

        public PatientApiHealthCheck(IOptionsMonitor<PatientOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.PatientApiUrl);
            try
            {
                Patient.PatientClient client = new(channel);
                PatientModelsMessage patientModelsMessage = client.GetAll(
                    new EmptyMessage(), cancellationToken: cancellationToken);

                return patientModelsMessage.Patients.Any()
                    ? Task.FromResult(HealthCheckResult.Healthy())
                    : Task.FromResult(HealthCheckResult.Unhealthy());
            }
            catch
            {
                return Task.FromResult(HealthCheckResult.Unhealthy());
            }
        }
    }
}
