using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    //public Patient GetById(Guid id);
}
