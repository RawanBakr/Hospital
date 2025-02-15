using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Infrastructure.UnitOfWork;

namespace Hospital.In.Repositories;

//[Authorize("Receptionst")]
public class PatientRepository : IPatientRepository
{

    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context= context;
    }

    public async Task AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await SaveAsync();
    }

    public async Task DeleteAsync(Patient patient)
    {
        _context.Patients.Remove(patient);
    }

    public IQueryable<Patient> GetPatientsAsync()
    {
        return _context.Patients.AsQueryable();
    }

    public async Task<IQueryable<PatientDTO>> GetAllPatientsAsync()
    {
        var query = _context.Patients.Select(p => new PatientDTO
        {
            Id = p.Id,
            Name = p.Name
        }).AsQueryable();

        return query;

        //var patients = await query.ToListAsync();

        //var patientDTOs = patients.Select(p => new PatientDTO
        //{
        //    Id= p.Id,
        //    Name= p.Name
        //}).ToList();

        //return patientDTOs;

        //var patients = await _context.Patients
        //.Select(p => new PatientDTO
        //{
        //    Id = p.Id,
        //    Name = p.Name
        //})
        //.ToListAsync();

    }

    public async Task<IQueryable<Patient>> GetAllAsync()
    {
        return _context.Patients.AsQueryable();
    }

    public async Task<PaginatedList<PatientDTO>> GetPaginatedPatientsAsync(int pageNumber, int pageSize)
    {

        int skip = (pageNumber - 1) * pageSize;
        var query = _context.Patients.Select(e => new PatientDTO()
        {
            Id=e.Id,
            Name=e.Name,
            Gender=e.Gender,
            UserName=e.UserName
        }).Skip(skip).Take(pageSize).AsQueryable();

        int totalCount = await _context.GetTotalPatientCountAsync();

        var patients = await query.ToListAsync();

        var patientDTOs = patients.Select(p => new PatientDTO
        {
            Id = p.Id,
            Name = p.Name,
            Gender= p.Gender,
            UserName = p.UserName,
        }).ToList();

        return new PaginatedList<PatientDTO>(patientDTOs, pageNumber, pageSize, totalCount);
    }

    public async Task<int> GetTotalPatientCountAsync()
    {
        return await _context.Patients.CountAsync();
    }

    public async Task<Patient> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Patients.FindAsync(id);
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new KeyNotFoundException("An error occurred while retrieving the patient.", ex);
        }
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public Task SaveAsync()
    {
        _context.SaveChanges();
        return Task.CompletedTask;
    }


    //public async Task<PaginatedList<PatientDTO>> GetPaginatedPatientsAsync(int pageNumber, int pageSize)
    //{
    //    var query = await _context.;
    //    int totalCount = await _patientRepository.GetTotalPatientCountAsync();

    //    int skip = (pageNumber - 1) * pageSize;
    //    var patients = await query.Skip(skip).Take(pageSize).ToListAsync();

    //    var patientDTOs = patients.Select(p => new PatientDTO
    //    {
    //        Id = p.Id,
    //        Name = p.Name,
    //        Gender= p.Gender,
    //        UserName = p.UserName,
    //    }).ToList();

    //    return new PaginatedList<PatientDTO>(patientDTOs, pageNumber, pageSize, totalCount);
    //}

    //public Patient GetById(Guid id)
    //{
    //    return _context.Patients.FirstOrDefault(e=>e.Id==id);
    //}
}
