using MedicalSystem.Admin.HealthCheckDashboard.Protos.Consultations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Admin.HealthCheckDashboard.ApiHealthChecks
{
    internal class ConsultationApiHealthCheck : IHealthCheck
    {
        private readonly Consultation.ConsultationClient _client;

        public ConsultationApiHealthCheck(Consultation.ConsultationClient client)
        {
            _client = client;
        }

        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                ConsultationModelsMessage consultationModelsMessage = _client.GetAll(
                    new EmptyMessage(), cancellationToken: cancellationToken);

                return consultationModelsMessage.Consultations.Any()
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
