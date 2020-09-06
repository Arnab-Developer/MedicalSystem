using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    /// <include file='docs.xml' path='docs/members[@name="IPatientGrpcClient"]/patientGrpcClient/*'/>
    public interface IPatientGrpcClient
    {
        /// <include file='docs.xml' path='docs/members[@name="IPatientGrpcClient"]/getAllAsync/*'/>
        Task<PatientModelsMessage> GetAllAsync(EmptyMessage request);
    }
}
