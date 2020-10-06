using System;

namespace MedicalSystem.Services.Consultation.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
