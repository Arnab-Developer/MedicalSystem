using MedicalSystem.Gateways.WebGateway.Protos.Patients;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Patients
{
    /// <include file='docs.xml' path='docs/members[@name="IPatientGrpcClient"]/patientGrpcClient/*'/>
    public interface IPatientGrpcClient
    {
        /// <include file='docs.xml' path='docs/members[@name="IPatientGrpcClient"]/getAllAsync/*'/>
        Task<PatientModelsMessage> GetAllAsync(EmptyMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IPatientGrpcClient"]/getByIdAsync/*'/>
        Task<PatientModelMessage> GetByIdAsync(IdMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IPatientGrpcClient"]/addAsync/*'/>
        Task AddAsync(PatientModelMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IPatientGrpcClient"]/updateAsync/*'/>
        Task UpdateAsync(UpdateMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IPatientGrpcClient"]/deleteAsync/*'/>
        Task DeleteAsync(IdMessage request);
    }
}
