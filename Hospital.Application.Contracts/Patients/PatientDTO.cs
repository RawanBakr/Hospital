﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Contracts.Patients;

public class PatientDTO
{
    public Guid Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Gender { get; set; }

    [StringLength(100)]
    public string UserName { get; set; }

    [StringLength(100)]
    public string Password { get; set; }
}
