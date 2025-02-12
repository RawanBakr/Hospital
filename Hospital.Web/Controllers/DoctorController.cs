using Hospital.Application.Contracts.Doctors;
using Hospital.Application.Contracts.Patients;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Application.Doctors;
using Hospital.Application.Patients;
using Microsoft.AspNetCore.Mvc;
using Octokit;

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
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var doctor = await _doctorAppService.GetDoctorId(id);

        if (doctor == null)
        {
            return NotFound();
        }

        var doctorDTO = new CreateUpdateDoctorDTO
        {
            Name = doctor.Name,
            Phone = doctor.Phone,
        };

        return View(doctorDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CreateUpdateDoctorDTO updateDoctor)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _doctorAppService.UpdateDoctor(id, updateDoctor);

                TempData["SuccessMessage"] = "Doctor updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
        }
        return View(updateDoctor);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var doctor = await _doctorAppService.GetDoctorId(id);

        if (doctor == null)
        {
            return NotFound();
        }
        return View(doctor);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            await _doctorAppService.DeleteDoctor(id);

            TempData["SuccessMessage"] = "Doctor deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var doctor = await _doctorAppService.GetDoctorId(id);

        if (doctor == null)
        {
            return NotFound(); 
        }

        return View(doctor);
    }
}
