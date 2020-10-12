using System;

namespace MedicalSystem.Services.Consultation.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
