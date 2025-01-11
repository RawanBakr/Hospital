using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities;

public class Doctor
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string? Phone { get; set; }
}
