using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Contracts.Interfaces;

public interface IPatientRepository : IRepository<Patient>
{
    IQueryable<Patient> GetPatientsAsync();
    Task<IQueryable<PatientDTO>> GetAllPatientsAsync();
    Task<int> GetTotalPatientCountAsync();
    Task<PaginatedList<PatientDTO>> GetPaginatedPatientsAsync(int pageNumber, int pageSize);
}
