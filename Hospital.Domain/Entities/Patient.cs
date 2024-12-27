using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities;

public class Patient
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

    public List<Notes>? Notes { get; set; }
}
