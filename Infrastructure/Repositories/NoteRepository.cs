using Hospital.Application.Contracts.Doctors;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Notes;
using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly ApplicationDbContext _context;

    public NoteRepository(ApplicationDbContext context)
    {
        _context=context;
    }

    public async Task AddAsync(Note note)
    {
        //var patients =_context.Patients.Select(p=> new PatientDTO
        //{
        //    Id =p.Id,
        //    Name =p.Name,
        //}).ToList();
        await _context.Notes.AddAsync(note);
        await SaveAsync();
    }

    public async Task DeleteAsync(Note note)
    {
        _context.Notes.Remove(note);
    }

    public async Task<IQueryable<Note>> GetAllAsync()
    {
        return _context.Notes.AsQueryable();
    }

    public async Task<PaginatedList<NoteDTO>> GetPaginatedNoteAsync(int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;
        var query = _context.Notes
            .Include(e => e.Patient)
            .Select(e => new NoteDTO()
            {
                Id=e.Id,
                Mediciness=e.Mediciness,
                Date=e.Date,
                PatientId=e.PatientId,
                PatientName=e.Patient.Name
            }).Skip(skip).Take(pageSize).AsQueryable();

        int totalCount = await _context.GetTotalNotesCountAsync();

        var notes = await query.ToListAsync();

        var noteDTOs = notes.Select(p => new NoteDTO
        {
            Id = p.Id,
            Mediciness = p.Mediciness,
            Date= p.Date,
            PatientId = p.PatientId,
            PatientName=p.PatientName
        }).ToList();

        return new PaginatedList<NoteDTO>(noteDTOs, pageNumber, pageSize, totalCount);
    }

    public async Task<Note> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Notes.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new KeyNotFoundException("An error occurred while retrieving the Note.", ex);
        }
    }

    public async Task<int> GetTotalNotesCountAsync()
    {
        return await _context.Notes.CountAsync();
    }

    public Task SaveAsync()
    {
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Note note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
    }

}
