using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Interfaces;  // ✅ Fixed (removed "System")
using ClinicManagement.Application.DTOs;        // ✅ Fixed (removed "System")
using ClinicManagement.Domain.Entities;         // ✅ Fixed (removed "System")

namespace ClinicManagement.API.Controllers      // ✅ Fixed namespace
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repo;

        public PatientsController(IPatientRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            var patients = await _repo.GetAllAsync(search);
            var result = patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age,
                Gender = p.Gender,
                Contact = p.Contact,
                LastProblem = p.Visits?.LastOrDefault()?.Problem ?? "No Recorded Visits",
                AssignedDoctor = p.Visits?.LastOrDefault()?.Doctor?.Name ?? "None",
                LastVisitDate = p.Visits?.LastOrDefault()?.VisitDate
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _repo.GetByIdAsync(id);
            if (patient == null) return NotFound(new { message = "Patient records not found." });
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Patient patient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repo.AddAsync(patient);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id) return BadRequest(new { message = "ID match failed." });

            await _repo.UpdateAsync(patient);
            var success = await _repo.SaveChangesAsync();
            if (!success) return BadRequest(new { message = "Could not update entity records." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            var success = await _repo.SaveChangesAsync();
            if (!success) return NotFound(new { message = "Patient record missing." });

            return Ok(new { message = "Record successfully removed." });
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            return Ok(await _repo.GetDoctorsAsync());
        }

        [HttpPost("visits")]
        public async Task<IActionResult> AddVisit([FromBody] Visit visit)
        {
            await _repo.AddVisitAsync(visit);
            await _repo.SaveChangesAsync();
            return Ok(new { message = "Medical visit logged successfully." });
        }
    }
}