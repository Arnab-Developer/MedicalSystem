using Grpc.Net.Client;
using MedicalSystem.Admin.HealthCheckDashboard.Options;
using MedicalSystem.Admin.HealthCheckDashboard.Protos.Consultations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Admin.HealthCheckDashboard.ApiHealthChecks
{
    internal class ConsultationApiHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<ConsultationOptions> _optionsAccessor;

        public ConsultationApiHealthCheck(IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.ConsultationApiUrl);
            try
            {
                Consultation.ConsultationClient client = new(channel);
                ConsultationModelsMessage consultationModelsMessage = client.GetAll(
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
