namespace MedicalSystem.Services.Consultation.SeedWork
{
    internal interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
