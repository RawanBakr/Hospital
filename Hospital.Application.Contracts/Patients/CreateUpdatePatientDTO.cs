using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Contracts.Patients;

public class CreateUpdatePatientDTO
{
    public Guid ID { get; set; }
  
    [Required]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
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
