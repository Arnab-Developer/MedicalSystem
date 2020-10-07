using MedicalSystem.Gateways.WebGateway.Protos.Patients;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Patients
{
    public interface IPatientGrpcClient
    {
        Task<PatientModelsMessage> GetAllAsync(EmptyMessage request);

        Task<PatientModelMessage> GetByIdAsync(IdMessage request);

        Task AddAsync(PatientModelMessage request);

        Task UpdateAsync(UpdateMessage request);

        Task DeleteAsync(IdMessage request);
    }
}
