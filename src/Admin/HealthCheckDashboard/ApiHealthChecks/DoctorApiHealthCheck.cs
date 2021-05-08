using MedicalSystem.Admin.HealthCheckDashboard.Protos.Doctors;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Admin.HealthCheckDashboard.ApiHealthChecks
{
    internal class DoctorApiHealthCheck : IHealthCheck
    {
        private readonly Doctor.DoctorClient _client;

        public DoctorApiHealthCheck(Doctor.DoctorClient client)
        {
            _client = client;
        }

        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                DoctorModelsMessage doctorModelsMessage = _client.GetAll(
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
