using System.ComponentModel.DataAnnotations;

namespace Hospital.Application.Contracts.Doctors;

public class DoctorDTO
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string? Phone { get; set; }
}
