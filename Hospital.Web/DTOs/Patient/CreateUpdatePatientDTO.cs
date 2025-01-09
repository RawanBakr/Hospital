using System.ComponentModel.DataAnnotations;

namespace Hospital.Web.DTOs.Patient;

public class CreateUpdatePatientDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Gender { get; set; }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
}
