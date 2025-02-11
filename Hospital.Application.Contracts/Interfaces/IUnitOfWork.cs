namespace Hospital.Application.Contracts.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPatientRepository Patients { get; }
    int Complete();
}
