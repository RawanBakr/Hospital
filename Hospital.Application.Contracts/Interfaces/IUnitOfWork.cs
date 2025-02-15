namespace Hospital.Application.Contracts.Interfaces;

public interface IUnitOfWork :IDisposable
{
    IPatientRepository Patients { get; }
    IDoctorRepository Doctors { get; }
    INoteRepository Notes { get; }
    int Complete();
}
