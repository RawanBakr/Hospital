using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Domain.Entities;
using Hospital.Application.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Application.Patients;

public class PatientAppService : IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IExceptionMiddlewareService _exceptionMiddlewareService;

    public PatientAppService(IUnitOfWork unitOfWork, IExceptionMiddlewareService exceptionMiddlewareService)
    {
        _unitOfWork = unitOfWork;
        _exceptionMiddlewareService = exceptionMiddlewareService;
    }

    public async Task<IQueryable<PatientDTO>> GetAllPatientsAsync()
    {
        var patientsQuery = _unitOfWork.Patients.GetPatientsAsync();

        var patientDtosQuery = patientsQuery.Select(p => new PatientDTO
        {
            Id = p.Id,
            Name = p.Name
        }).AsQueryable();

        return patientDtosQuery;
    }

    public async Task<PaginatedList<PatientDTO>> GetPaginatedPatientsAsync(int pageNumber, int pageSize)
    {

        return await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            return await _unitOfWork.Patients.GetPaginatedPatientsAsync(pageNumber, pageSize);
        });
    }

    public async Task<PatientDTO> CreatePatient(CreateUpdatePatientDTO createPatient)
    {
        return await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var patient = new Patient
            {
                Id=createPatient.ID,
                Name = createPatient.Name,
                Gender = createPatient.Gender,
                UserName = createPatient.UserName,
                Password = createPatient.Password
            };

            ExceptionMiddlewareService.ValidatePassword(patient.Password);
            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.Patients.SaveAsync();

            var patientDto = new PatientDTO
            {
                Id = patient.Id,
                Name = patient.Name,
                Gender = patient.Gender,
                UserName = patient.UserName,
                Password = patient.Password
            };

            return patientDto;
        });
    }

    public async Task UpdatePatient(Guid id, CreateUpdatePatientDTO updatePatient)
    {

        await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }

            patient.Name = updatePatient.Name;
            patient.Gender = updatePatient.Gender;
            patient.UserName = updatePatient.UserName;
            patient.Password = updatePatient.Password;

            await _unitOfWork.Patients.UpdateAsync(patient);
        });
    }

    public async Task<PatientDTO> GetPatientId(Guid id)
    {
        return await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            if (patient == null)
            {
                throw new KeyNotFoundException("this patient ID not found in DB");
            }

            var patientDto = new PatientDTO
            {
                Id = patient.Id,
                Name = patient.Name,
                Gender = patient.Gender,
                UserName = patient.UserName,
                Password = patient.Password
            };
            return patientDto;
        });
    }

    public async Task DeletePatient(Guid id)
    {
        await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }
            await _unitOfWork.Patients.DeleteAsync(patient);
            await _unitOfWork.Patients.SaveAsync();
        });
    }
}
