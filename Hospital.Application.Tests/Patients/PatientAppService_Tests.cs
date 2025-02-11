//using Hospital.Application.Contracts.Patients;
//using Hospital.Application.Patients;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hospital.Application.Tests.Patients;

//public abstract class PatientAppService_Tests
//{
//    private readonly IPatientAppService<PatientDTO,Guid,CreateUpdatePatientDTO> _patientAppService;

//    protected PatientAppService_Tests(IPatientAppService<PatientDTO,Guid,CreateUpdatePatientDTO> patientAppService)
//    {
//        _patientAppService=patientAppService;
//    }

   
//    public async Task Should_Not_Create_A_Patient_Without_Name()
//    {
//        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
//        {
//            await _patientAppService.CreateAsync(
//                new CreateUpdateBookDto
//                {
//                    Name = "",
//                    Price = 10,
//                    PublishDate = DateTime.Now,
//                    Type = BookType.ScienceFiction
//                }
//            );
//        });

//        exception.ValidationErrors
//            .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
//    }

//}
