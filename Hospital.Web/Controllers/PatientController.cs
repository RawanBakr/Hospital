using Hospital.Application.Contracts.Pagination;
using Hospital.Application.Contracts.Patients;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Application.Patients;
using Hospital.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace Hospital.Web.Controllers;

public class PatientController : Controller
{
    private readonly IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO> _patientAppService;
    private readonly IExceptionMiddlewareService _exceptionMiddlewareService;

    public PatientController(IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO> patientAppService, IExceptionMiddlewareService exceptionMiddlewareService)
    {
        _patientAppService = patientAppService;
        _exceptionMiddlewareService= exceptionMiddlewareService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var paginatedPatients = await _patientAppService.GetPaginatedPatientsAsync(pageNumber, pageSize);
        return View(paginatedPatients);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdatePatientDTO createPatientDto)
    {
        try
        {
            var patientDto = await _patientAppService.CreatePatient(createPatientDto);

            ExceptionMiddlewareService.ValidatePassword(patientDto.Password);

            TempData["SuccessMessage"] = "Patient created successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Create));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while creating the patient. Please try again.";
            return RedirectToAction(nameof(Create));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var patient = await _patientAppService.GetPatientId(id);

        if (patient == null)
        {
            return NotFound();
        }

        var patientDto = new CreateUpdatePatientDTO
        {
            Name = patient.Name,
            Gender = patient.Gender,
            UserName = patient.UserName,
            Password = patient.Password
        };

        return View(patientDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CreateUpdatePatientDTO updatePatient)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _patientAppService.UpdatePatient(id, updatePatient);

                TempData["SuccessMessage"] = "Patient updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }
        }

        return View(updatePatient);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var patient = await _patientAppService.GetPatientId(id);
        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            await _patientAppService.DeletePatient(id);
            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deleting the patient.");
        }
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var patient = await _patientAppService.GetPatientId(id);

        if (patient == null)
        {
            return NotFound();
        }

        return View(patient);
    }

}
