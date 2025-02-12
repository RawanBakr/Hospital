using Hospital.Application.Contracts.Doctors;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Domain.Entities;

namespace Hospital.Application.Doctors;

public class DoctorAppServie : IDoctorAppService<DoctorDTO, Guid, CreateUpdateDoctorDTO>
{

    private readonly IUnitOfWork _unitOfWork;

    private readonly IExceptionMiddlewareService _exceptionMiddlewareService;

    public DoctorAppServie(IUnitOfWork unitOfWork, IExceptionMiddlewareService exceptionMiddlewareService)
    {
        _unitOfWork = unitOfWork;
        _exceptionMiddlewareService = exceptionMiddlewareService;
    }

    public async Task<PaginatedList<DoctorDTO>> GetPaginatedDoctorsAsync(int pageNumber, int pageSize)
    {

        return await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            return await _unitOfWork.Doctors.GetPaginatedDoctorsAsync(pageNumber, pageSize);
        });
    }
    public async Task<DoctorDTO> CreateDoctor(CreateUpdateDoctorDTO createDoctor)
    {
        return await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var doctor = new Doctor
            {
                Id=createDoctor.DoctorID,
                Name = createDoctor.Name,
                Phone = createDoctor.Phone
            };

            await _unitOfWork.Doctors.AddAsync(doctor);
            await _unitOfWork.Doctors.SaveAsync();

            var doctorDTO = new DoctorDTO
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Phone = doctor.Phone,
            };
            return doctorDTO;
        });
    }
    public async Task UpdateDoctor(Guid id, CreateUpdateDoctorDTO updateDoctor)
    {
        await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found.");
            }

            doctor.Name = updateDoctor.Name;
            doctor.Phone = updateDoctor.Phone;

            await _unitOfWork.Doctors.UpdateAsync(doctor);
        });
    }

    public async Task<DoctorDTO> GetDoctorId(Guid id)
    {
        return await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (doctor == null)
            {
                throw new KeyNotFoundException("this doctor ID not found in DB");
            }

            var doctorDTO = new DoctorDTO
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Phone = doctor.Phone,
            };
            return doctorDTO;
        });
    }

    public async Task DeleteDoctor(Guid id)
    {
        await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found.");
            }

            await _unitOfWork.Doctors.DeleteAsync(doctor);
            await _unitOfWork.Doctors.SaveAsync();
        });
    }

}
