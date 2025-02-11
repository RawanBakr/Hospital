using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Patients;

public class PatientAppService : IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO>
{
    private readonly IPatientRepository _patientRepository;

    private readonly IExceptionMiddlewareService _exceptionMiddlewareService;

    public PatientAppService(IPatientRepository patientRepository, IExceptionMiddlewareService exceptionMiddlewareService)
    {
        _patientRepository = patientRepository;
        _exceptionMiddlewareService = exceptionMiddlewareService;
    }

    public async Task<PaginatedList<PatientDTO>> GetPaginatedPatientsAsync(int pageNumber, int pageSize)
    {

        return await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            return await _patientRepository.GetPaginatedPatientsAsync(pageNumber, pageSize);
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
            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveAsync();

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
            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }

            patient.Name = updatePatient.Name;
            patient.Gender = updatePatient.Gender;
            patient.UserName = updatePatient.UserName;
            patient.Password = updatePatient.Password;

            await _patientRepository.UpdateAsync(patient);
        });
    }

    public async Task<PatientDTO> GetPatientId(Guid id)
    {
        return await _exceptionMiddlewareService.ExecuteAsync(async () =>
        {
            var patient = await _patientRepository.GetByIdAsync(id);

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
            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }
            await _patientRepository.DeleteAsync(patient);
            await _patientRepository.SaveAsync();
        });
    }

}
