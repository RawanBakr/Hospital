﻿using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Repositories;

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
         SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Patients.OrderBy(e=>e.Name).ToListAsync();

        //IQueryable<Patient> queryable = _context.Patients.Include(e => new { e.Name, e.Gender }).OrderBy(e => e.Id);
        //return await queryable.ToListAsync();
    }

    public async Task<Patient> GetByIdAsync(Guid id)
    {
       return await _context.Patients.FirstOrDefaultAsync(e=>e.Id ==id);
     
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


    //public IEnumerable<Patient> GetStaticPatients()
    //{
    //    return new List<Patient>
    //    {
    //        new Patient {  Name = "John Doe", Gender = "male",UserName="kkkk",Password="JHJJHIOIH" }
    //    };
    //}

    //public void Add(Patient patient)
    //{
    //    //_context.Patients.Add(patient);
    //    //_context.SaveChanges();
    //}

    //public void Delete(Patient patient)
    //{
    //    //_context.Patients.Remove(patient);
    //}

    //public Patient Find(Expression<Func<Patient, bool>> predicate, string[]? includes = null)
    //{
    //    //IQueryable<Patient> query = _context.Patients;
    //    //if (query == null)
    //    //    foreach (var include in includes)
    //    //        query=query.Include(include);
    //    //return query.SingleOrDefault(predicate);
    //}

    //public IEnumerable<Patient> GetAll()
    //{
    //    //return _context.Patients;
    //}

    //public Patient GetById(Guid id)
    //{
    //    //return _context.Patients.Find(id);
    //}

    //public void Update(Patient patient)
    //{
    //    //if (patient != null)
    //    //{
    //    //    _context.Update(patient);
    //    //}
    //    //_context.SaveChanges(true);

    //}

    //IEnumerable<Patient> IPatientRepository.GetAll()
    //{
    //    throw new NotImplementedException();
    //}
}