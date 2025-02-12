using Hospital.Application.Contracts.Doctors;
using Hospital.Application.Contracts.Patients;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Application.Doctors;
using Hospital.Application.Patients;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers;

public class DoctorController : Controller
{
    private readonly IDoctorAppService<DoctorDTO, Guid, CreateUpdateDoctorDTO> _doctorAppService;

    public DoctorController(IDoctorAppService<DoctorDTO, Guid, CreateUpdateDoctorDTO> doctorAppService)
    {
        _doctorAppService=doctorAppService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var paginatedPatients = await _doctorAppService.GetPaginatedDoctorsAsync(pageNumber, pageSize);
        return View(paginatedPatients);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateDoctorDTO createDoctorDTO)
    {
        try
        {
            var patientDto = await _doctorAppService.CreateDoctor(createDoctorDTO);

            TempData["SuccessMessage"] = "Doctor created successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Create));
        }
        //catch (Exception ex)
        //{
        //    TempData["ErrorMessage"] = "An error occurred while creating the patient. Please try again.";
        //    return RedirectToAction(nameof(Create));
        //}
    }
}
