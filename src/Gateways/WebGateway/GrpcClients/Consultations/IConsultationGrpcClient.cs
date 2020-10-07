using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    public interface IConsultationGrpcClient
    {
        Task<ConsultationModelsMessage> GetAllAsync(EmptyMessage request);

        Task<ConsultationModelMessage> GetByIdAsync(IdMessage request);

        Task AddAsync(ConsultationModelMessage request);

        Task UpdateAsync(UpdateMessage request);

        Task DeleteAsync(IdMessage request);
    }
}
