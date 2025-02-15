using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
namespace Hospital.Application.Patients;

public interface IPatientAppService<PatientDto, Guid, CreateUpdatePatientDTO>
{
    Task<IQueryable<PatientDTO>> GetAllPatientsAsync();
    Task<PatientDto> CreatePatient(CreateUpdatePatientDTO createPatient);
    Task UpdatePatient(Guid id, CreateUpdatePatientDTO updatePatient);
    Task<PatientDto> GetPatientId(Guid id);
    Task DeletePatient(Guid id);
    Task<PaginatedList<PatientDTO>> GetPaginatedPatientsAsync(int pageNumber, int pageSize);
}
