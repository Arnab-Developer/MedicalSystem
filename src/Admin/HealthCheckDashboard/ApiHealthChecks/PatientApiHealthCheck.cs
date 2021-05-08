using MedicalSystem.Admin.HealthCheckDashboard.Protos.Patients;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Admin.HealthCheckDashboard.ApiHealthChecks
{
    internal class PatientApiHealthCheck : IHealthCheck
    {
        private readonly Patient.PatientClient _client;

        public PatientApiHealthCheck(Patient.PatientClient client)
        {
            _client = client;
        }

        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                PatientModelsMessage patientModelsMessage = _client.GetAll(
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
