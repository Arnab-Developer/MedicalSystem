using Grpc.Net.Client;
using MedicalSystem.Admin.HealthCheckDashboard.Options;
using MedicalSystem.Admin.HealthCheckDashboard.Protos.Doctors;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Admin.HealthCheckDashboard.ApiHealthChecks
{
    internal class DoctorApiHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<DoctorOptions> _optionsAccessor;

        public DoctorApiHealthCheck(IOptionsMonitor<DoctorOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            using GrpcChannel channel = GrpcChannel.ForAddress(_optionsAccessor.CurrentValue.DoctorApiUrl);
            try
            {
                Doctor.DoctorClient client = new(channel);
                DoctorModelsMessage doctorModelsMessage = client.GetAll(
                    new EmptyMessage(), cancellationToken: cancellationToken);

                return doctorModelsMessage.Doctors.Any()
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
