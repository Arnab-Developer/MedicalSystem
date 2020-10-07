using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    public interface IPatientGrpcClient
    {
        Task<PatientModelsMessage> GetAllAsync(EmptyMessage request);
    }
}
