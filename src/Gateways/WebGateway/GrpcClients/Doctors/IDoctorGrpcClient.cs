using MedicalSystem.Gateways.WebGateway.Protos.Doctors;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors
{
    /// <include file='docs.xml' path='docs/members[@name="IDoctorGrpcClient"]/doctorGrpcClient/*'/>
    public interface IDoctorGrpcClient
    {
        /// <include file='docs.xml' path='docs/members[@name="IDoctorGrpcClient"]/getAllAsync/*'/>
        Task<DoctorModelsMessage> GetAllAsync(EmptyMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IDoctorGrpcClient"]/getByIdAsync/*'/>
        Task<DoctorModelMessage> GetByIdAsync(IdMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IDoctorGrpcClient"]/addAsync/*'/>
        Task AddAsync(DoctorModelMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IDoctorGrpcClient"]/updateAsync/*'/>
        Task UpdateAsync(UpdateMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IDoctorGrpcClient"]/deleteAsync/*'/>
        Task DeleteAsync(IdMessage request);
    }
}
