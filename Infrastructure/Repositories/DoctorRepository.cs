using Hospital.Application.Contracts.Doctors;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{

    private readonly ApplicationDbContext _context;

    public DoctorRepository(ApplicationDbContext context)
    {
        _context=context;
    }

    public async Task AddAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
        await SaveAsync();
    }

    public async Task DeleteAsync(Doctor doctor)
    {
       _context.Doctors.Remove(doctor);
    }

    public async Task<IQueryable<Doctor>> GetAllAsync()
    {
        return _context.Doctors.AsQueryable();
    }

    public async Task<Doctor> GetByIdAsync(Guid id)
    {

        try
        {
            return await _context.Doctors.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new KeyNotFoundException("An error occurred while retrieving the Doctor.", ex);
        }
    }

    public async Task<PaginatedList<DoctorDTO>> GetPaginatedDoctorsAsync(int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;
        var query = _context.Doctors.Select(e => new DoctorDTO()
        {
            Id=e.Id,
            Name=e.Name,
            Phone=e.Phone,
        }).Skip(skip).Take(pageSize).AsQueryable();

        int totalCount = await _context.GetTotalDoctorCountAsync();

        var doctors = await query.ToListAsync();

        var doctorDTOs = doctors.Select(p => new DoctorDTO
        {
            Id = p.Id,
            Name = p.Name,
            Phone= p.Phone,
        }).ToList();

        return new PaginatedList<DoctorDTO>(doctorDTOs, pageNumber, pageSize, totalCount);
    }

    public async Task<int> GetTotalDoctorCountAsync()
    {
        return await _context.Patients.CountAsync();
    }

    public Task SaveAsync()
    {
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }
}
