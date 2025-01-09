using Hospital.Application.Repositories;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using Hospital.Web.DTOs.Patient;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace Hospital.Web.Controllers
{
 
    public class PatientController : Controller
    {

        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var patients = await _patientRepository.GetAllAsync();
            var patientDto = patients.Select(patient => new PatientDTO
            {
                Name = patient.Name,
                Gender = patient.Gender,
                UserName = patient.UserName,
                Password = patient.Password,
            }).ToList();
            return View(patientDto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdatePatientDTO patientDto)
        {
            if (!ModelState.IsValid)
            {
                return View(patientDto); 
            }
            var patient = new Patient
            {
                Name = patientDto.Name,
                Gender = patientDto.Gender,
                UserName = patientDto.UserName,
                Password = patientDto.Password
            };

            await _patientRepository.AddAsync(patient);
             await _patientRepository.SaveAsync();

            return RedirectToAction("Index");
        }

    }
}
