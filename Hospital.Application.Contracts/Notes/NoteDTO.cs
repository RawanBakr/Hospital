using Hospital.Domain.Entities;

namespace Hospital.Application.Contracts.Notes;

public class NoteDTO
{
    public Guid Id { get; set; }

    public string Mediciness { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public Guid PatientId { get; set; }
    public string? PatientName { get; set; }
}
