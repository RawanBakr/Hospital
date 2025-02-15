using Hospital.Application.Contracts.Notes;
using Hospital.Application.Contracts.Patients;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Application.Notes;
using Hospital.Application.Patients;
using Hospital.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Octokit;

namespace Hospital.Web.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteAppService<NoteDTO, Guid, CreateUpdateNoteDTO, PatientDTO> _noteAppService;
        private readonly IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO> _patientAppService;

        public NoteController(INoteAppService<NoteDTO, Guid, CreateUpdateNoteDTO, PatientDTO> noteAppService
            , IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO> patientAppService)
        {
            _noteAppService=noteAppService;
            _patientAppService=patientAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var paginatedNotes = await _noteAppService.GetPaginatedNotesAsync(pageNumber, pageSize);
            return View(paginatedNotes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var patientsQuery = await _patientAppService.GetAllPatientsAsync();

            var patients = patientsQuery.ToList();

            var patientList = patients.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.PatientList = patientList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateNoteDTO createNoteDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var noteDTO = await _noteAppService.CreateNote(createNoteDTO);
                    TempData["SuccessMessage"] = "Note created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the note.");

                    return View(createNoteDTO);
                }
            }
            return View(createNoteDTO);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var note = await _noteAppService.GetNoteId(id);

            if (note == null)
            {
                return NotFound();
            }
            var patientsQuery = await _patientAppService.GetAllPatientsAsync();

            var patients = patientsQuery.ToList();

            var patientList = patients.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.PatientList = patientList;
            return View();

            //var noteDTO = new CreateUpdateNoteDTO
            //{
            //    Date = note.Date,
            //    Mediciness = note.Mediciness,
            //    PatientId = note.PatientId,
            //};

            //return View(noteDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreateUpdateNoteDTO updateNote)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _noteAppService.UpdateNote(id, updateNote);

                    TempData["SuccessMessage"] = "Note updated successfully!";

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

            return View(updateNote);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var note = await _noteAppService.GetNoteId(id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _noteAppService.DeleteNote(id);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the note.");
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var note = await _noteAppService.GetNoteId(id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }
    }
}
