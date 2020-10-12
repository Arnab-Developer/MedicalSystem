using MediatR;
using MedicalSystem.Services.Consultation.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalSystem.Services.Consultation.Api.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ConsultationContext _consultationContext;

        public TransactionBehaviour(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        async Task<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(TRequest request,
            CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using var transaction = _consultationContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            TResponse response = await next();
            transaction.Commit();
            return response;
        }
    }
}
