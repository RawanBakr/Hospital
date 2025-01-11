using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Application.Patients;

public interface IPatientAppService<PatientDto, Guid, CreateUpdatePatientDTO>
{
    Task<IEnumerable<PatientDto>> GetPatientList(PatientDto patientDto);
    Task<PatientDto> CreatePatient(CreateUpdatePatientDTO createPatient);
    Task UpdatePatient(Guid id, CreateUpdatePatientDTO updatePatient);
    Task<PatientDto> GetPatientId(Guid id);

}
