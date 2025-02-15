using Hospital.Application.Contracts.Doctors;
using Hospital.Application.Contracts.Notes;
using Hospital.Application.Contracts.Pagination;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;

namespace Hospital.Application.Contracts.Interfaces;

public interface INoteRepository :IRepository<Note>
{
    Task<int> GetTotalNotesCountAsync();
    Task<PaginatedList<NoteDTO>> GetPaginatedNoteAsync(int pageNumber, int pageSize);
}
