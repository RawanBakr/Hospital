using Hospital.Application.Contracts.Notes;
using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;

namespace Hospital.Application.Notes;

public interface INoteAppService<NoteDTO, Guid, CreateUpdateNoteDTO, PatientDTO>
{

    Task<CreateUpdateNoteDTO> GetPatientsAsync();

    Task<NoteDTO> CreateNote(CreateUpdateNoteDTO createNote);
    Task UpdateNote(Guid id, CreateUpdateNoteDTO updateNote);
    Task<NoteDTO> GetNoteId(Guid id);
    Task DeleteNote(Guid id);
    Task<PaginatedList<NoteDTO>> GetPaginatedNotesAsync(int pageNumber, int pageSize);
}
