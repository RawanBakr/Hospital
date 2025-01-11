using Hospital.Application.Contracts.Patients;
using Hospital.Application.Patients;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Controllers;

public class PatientController : Controller
{
    private readonly IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO> _patientAppService;

    public PatientController(IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO> patientAppService)
    {
        _patientAppService = patientAppService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var patientDTO = new PatientDTO();
        var patientList = await _patientAppService.GetPatientList(patientDTO);
        return View(patientList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdatePatientDTO createPatientDto)
    {
        if (!ModelState.IsValid)
        {
            return View(createPatientDto);
        }

        var patientDto = await _patientAppService.CreatePatient(createPatientDto);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var patient = await _patientAppService.GetPatientId(id);

        if (patient == null)
        {
            return NotFound();
        }

        // Map the patient entity to a DTO for the view
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


}
