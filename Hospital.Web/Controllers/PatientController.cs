using Hospital.Application.Repositories;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace Hospital.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {

        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var patients = await _patientRepository.GetAllAsync();
            return View(patients);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
               await _patientRepository.AddAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["errorMessage"]="Invalid Patient";
                return View(patient);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Patient patient)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _patientRepository.Add(patient);
        //            _patientRepository.sa();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (DataException)
        //    {
        //        ModelState.AddModelError("", "Unable to save changes. Try again. ");
        //    }

        //    return View(patient);
        //}

        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    var patient = await _patientRepository.GetByIdAsync(id);
        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patient);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, Patient patient)
        //{
        //    if (id != patient.Id)
        //    {
        //        return BadRequest("Patient ID mismatch.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _patientRepository.UpdateAsync(patient);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle errors like concurrency issues
        //            ModelState.AddModelError(string.Empty, "Unable to save changes. Try again.");
        //        }
        //    }

        //    return View(patient);
        //}

        //public async Task<IActionResult> Details(Guid id)
        //{
        //    Console.WriteLine($"Details called with ID: {id}"); // Log the ID
        //    var patient = await _patientRepository.GetByIdAsync(id);

        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patient);
        //}






        //[HttpGet]
        //public IActionResult StaticData()
        //{
        //    var patients = _patientRepository.GetStaticPatients();
        //    return Ok( patients);
        //}

        //private readonly IPatientRepository _patientRepository;

        //private readonly PatientRepository _patientRepository;

        //public PatientController(PatientRepository patientRepository)
        //{
        //    _patientRepository = patientRepository;
        //}

        //[HttpGet("static-data")]
        //public IActionResult GetStaticData(object staticData)
        //{


        //    return Ok(staticData);
        //}

        //[HttpGet("GetAll")]
        //public IActionResult Index() 
        //{
        //    var patients= _patientRepository.GetAll();
        //    return Ok(); ;

        //}

        //[HttpGet("GetByName")]
        //public IActionResult GetByName() 
        //{
        //    return Ok ( _patientRepository.Find(e => e.Name =="mona" , new[] { "Mediciness" }));
        //}
    }
}
