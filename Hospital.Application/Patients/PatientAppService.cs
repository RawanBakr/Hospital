using Hospital.Application.Contracts.Patients;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Patients;

public class PatientAppService : IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO>
{
    private readonly IPatientRepository _patientRepository;

    public PatientAppService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientDTO> CreatePatient(CreateUpdatePatientDTO createPatient)
    {
        var patient = new Patient
        {
            Id=createPatient.ID,
            Name = createPatient.Name,
            Gender = createPatient.Gender,
            UserName = createPatient.UserName,
            Password = createPatient.Password
        };

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
    }

    public async Task<IEnumerable<PatientDTO>> GetPatientList(PatientDTO patientDTO)
    {
        var patients = await _patientRepository.GetAllAsync();
        var patientDto = patients.Select(patient => new PatientDTO
        {
            Name = patient.Name,
            Gender = patient.Gender,
            UserName = patient.UserName,
            Password = patient.Password,
        }).ToList();

        return patientDto;
    }

    public async Task UpdatePatient(Guid id, CreateUpdatePatientDTO updatePatient)
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
    }

    public async Task<PatientDTO> GetPatientId(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);

        if (patient == null)
        {
            return null;
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
    }
}
