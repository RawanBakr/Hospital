using Hospital.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Application.Contracts.Notes;

public class CreateUpdateNoteDTO
{
    [Required]
    public Guid ID { get; set; }

    [StringLength(500)]
    public string Mediciness { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public Guid PatientId { get; set; }

    //public string PatientName { get; set; }

    //public SelectList Patients { get; set; }


    //public IEnumerable<Patient> Patients { get; set; }
}
