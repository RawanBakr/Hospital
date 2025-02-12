using Hospital.Application.Contracts.Doctors;
using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;

namespace Hospital.Application.Contracts.Interfaces;

public interface IDoctorRepository :IRepository<Doctor>
{
    Task<int> GetTotalDoctorCountAsync();
    Task<PaginatedList<DoctorDTO>> GetPaginatedDoctorsAsync(int pageNumber, int pageSize);
}
