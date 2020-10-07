using MedicalSystem.Gateways.WebGateway.Protos.Doctors;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors
{
    public interface IDoctorGrpcClient
    {
        Task<DoctorModelsMessage> GetAllAsync(EmptyMessage request);

        Task<DoctorModelMessage> GetByIdAsync(IdMessage request);

        Task AddAsync(DoctorModelMessage request);

        Task UpdateAsync(UpdateMessage request);

        Task DeleteAsync(IdMessage request);
    }
}
