using Hospital.Application.Contracts.Interfaces;
using Hospital.Domain.Entities;
using Hospital.In.Repositories;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Repositories;

namespace Hospital.Infrastructure.UnitOfWork;

public class UnitOfWork :IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Patients= new PatientRepository(_context);
        Doctors = new DoctorRepository(_context);
    }

    public IPatientRepository Patients { get; set; }
    public IDoctorRepository Doctors { get; set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
