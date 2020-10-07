using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    public interface IDoctorGrpcClient
    {
        Task<DoctorModelsMessage> GetAllAsync(EmptyMessage request);
    }
}
