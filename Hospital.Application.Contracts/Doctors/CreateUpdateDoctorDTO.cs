using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Contracts.Doctors;

public class CreateUpdateDoctorDTO
{
    public Guid DoctorID { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Dotor name cannot exceed 100 characters.")]
    public string Name { get; set; }

    [MaxLength(100, ErrorMessage = "Phone Number cannot exceed 100 characters.")]
    public string? Phone { get; set; }
}
