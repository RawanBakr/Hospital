using Hospital.Application.Contracts.Interfaces;
using Hospital.In.Repositories;
using Hospital.Infrastructure.Data;

namespace Hospital.Infrastructure.UnitOfWork;

public class UnitOfWork :IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Patients= new PatientRepository(_context);
    }

    public IPatientRepository Patients { get; set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
