using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Notes;
using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Notes;

public class NoteAppService : INoteAppService<NoteDTO, Guid, CreateUpdateNoteDTO, PatientDTO>
{
    private readonly IUnitOfWork _unitOfWork;

    public NoteAppService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }

    public async Task<CreateUpdateNoteDTO> GetPatientsAsync()
    {
        var patients = await _unitOfWork.Patients.GetAllPatientsAsync();

        return new CreateUpdateNoteDTO();

    }

    public async Task<PaginatedList<NoteDTO>> GetPaginatedNotesAsync(int pageNumber, int pageSize)
    {
        return await _unitOfWork.Notes.GetPaginatedNoteAsync(pageNumber, pageSize);
    }

    public async Task<NoteDTO> CreateNote(CreateUpdateNoteDTO createNote)
    {
        var note = new Note
        {
            Id=createNote.ID,
            Mediciness = createNote.Mediciness,
            Date = createNote.Date,
            PatientId = createNote.PatientId,
        };

        await _unitOfWork.Notes.AddAsync(note);
        await _unitOfWork.Notes.SaveAsync();

        var noteDTO = new NoteDTO
        {
            Id = note.Id,
            Mediciness = note.Mediciness,
            Date = note.Date,
            PatientId = note.PatientId,
        };

        return noteDTO;
    }

    public async Task UpdateNote(Guid id, CreateUpdateNoteDTO updateNote)
    {
        var note = await _unitOfWork.Notes.GetByIdAsync(id);

        if (note == null)
        {
            throw new KeyNotFoundException("Note not found.");
        }

        note.Date = updateNote.Date;
        note.Mediciness = updateNote.Mediciness;
        note.PatientId = updateNote.PatientId;

        await _unitOfWork.Notes.UpdateAsync(note);
    }

    public async Task<NoteDTO> GetNoteId(Guid id)
    {
        var note = await _unitOfWork.Notes.GetByIdAsync(id);

        if (note == null)
        {
            throw new KeyNotFoundException("this Note ID not found in DB");
        }

        var noteDTO = new NoteDTO
        {
            Id = note.Id,
            Date = note.Date,
            Mediciness = note.Mediciness,
            PatientId = note.PatientId,
        };
        return noteDTO;
    }

    public async Task DeleteNote(Guid id)
    {
        var note = await _unitOfWork.Patients.GetByIdAsync(id);

        if (note == null)
        {
            throw new KeyNotFoundException("Note not found.");
        }
        await _unitOfWork.Patients.DeleteAsync(note);
        await _unitOfWork.Patients.SaveAsync();
    }


}
