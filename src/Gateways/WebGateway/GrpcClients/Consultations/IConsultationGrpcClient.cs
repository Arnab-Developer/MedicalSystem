using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations
{
    /// <include file='docs.xml' path='docs/members[@name="IConsultationGrpcClient"]/consultationGrpcClient/*'/>
    public interface IConsultationGrpcClient
    {
        /// <include file='docs.xml' path='docs/members[@name="IConsultationGrpcClient"]/getAllAsync/*'/>
        Task<ConsultationModelsMessage> GetAllAsync(EmptyMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationGrpcClient"]/getByIdAsync/*'/>
        Task<ConsultationModelMessage> GetByIdAsync(IdMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationGrpcClient"]/addAsync/*'/>
        Task AddAsync(ConsultationModelMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationGrpcClient"]/updateAsync/*'/>
        Task UpdateAsync(UpdateMessage request);

        /// <include file='docs.xml' path='docs/members[@name="IConsultationGrpcClient"]/deleteAsync/*'/>
        Task DeleteAsync(IdMessage request);
    }
}
