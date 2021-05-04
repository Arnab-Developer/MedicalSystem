using Grpc.Net.Client;
using MedicalSystem.Admin.HealthCheckDashboard.Protos.Patients;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Admin.HealthCheckDashboard.ApiHealthChecks
{
    internal class PatientApiHealthCheck : IHealthCheck
    {
        Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            using GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5001");
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
