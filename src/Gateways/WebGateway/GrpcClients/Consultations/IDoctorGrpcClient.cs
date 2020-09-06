using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    /// <include file='docs.xml' path='docs/members[@name="IDoctorGrpcClient"]/doctorGrpcClient/*'/>
    public interface IDoctorGrpcClient
    {
        /// <include file='docs.xml' path='docs/members[@name="IDoctorGrpcClient"]/getAllAsync/*'/>
        Task<DoctorModelsMessage> GetAllAsync(EmptyMessage request);
    }
}
