using System;

namespace MedicalSystem.Services.Consultation.SeedWork
{
    internal interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
