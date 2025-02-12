using Hospital.Application.Contracts.Pagination;

namespace Hospital.Application.Doctors;

public interface IDoctorAppService<DoctorDTO,Guid, CreateUpdateDoctorDTO>
{
    Task<DoctorDTO> CreateDoctor(CreateUpdateDoctorDTO createDoctor);
    Task UpdateDoctor(Guid id, CreateUpdateDoctorDTO updateDoctor);
    Task<DoctorDTO> GetDoctorId(Guid id);
    Task DeleteDoctor(Guid id);
    Task<PaginatedList<DoctorDTO>> GetPaginatedDoctorsAsync(int pageNumber, int pageSize);
}
