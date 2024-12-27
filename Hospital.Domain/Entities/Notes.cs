using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities;

public class Notes
{
    public Guid Id { get; set; }

    [MaxLength(500)]
    public string Mediciness { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;

    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    //object of patient   select one patient 
}
