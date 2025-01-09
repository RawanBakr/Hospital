using Hospital.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs.Patient;

public class PatientDTO
{
    public Guid Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string Gender { get; set; }

    [MaxLength(100)]
    public string UserName { get; set; }

    [MaxLength(100)]
    public string Password { get; set; }
}
