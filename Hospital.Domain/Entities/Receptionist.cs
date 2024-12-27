using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Entities;

public class Receptionist 
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Emaill { get; set; }
}
