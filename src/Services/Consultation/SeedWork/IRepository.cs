namespace MedicalSystem.Services.Consultation.SeedWork
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
