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

    public string Name { get; set; }
    public string Gender { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    //public DateTime DateCreated { get; set; }
    public ICollection<Note>? Notes { get; set; }
}
