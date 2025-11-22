using Backend.Data;
using Backend.Models.DTOs;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DoctorDto>>> GetAll()
    {
        var doctors = await context.Doctors
            .Include(d => d.Patients)
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                FullName = d.FullName,
                Age = d.Age,
                Experience = d.Experience,
                Specialty = d.Specialty,
                Patients = d.Patients.Select(p => p.FullName).ToList()
            })
            .ToListAsync();
        return Ok(doctors);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DoctorDto>> GetById(Guid id)
    {
        var doctor = await context.Doctors
            .Include(d => d.Patients)
            .Where(d => d.Id == id)
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                FullName = d.FullName,
                Age = d.Age,
                Experience = d.Experience,
                Specialty = d.Specialty,
                Patients = d.Patients.Select(p => p.FullName).ToList()
            })
            .FirstOrDefaultAsync();

        return doctor is null ? NotFound() : Ok(doctor);
    }

    [HttpPost]
    public async Task<ActionResult<DoctorDto>> Create(Doctor doctor)
    {
        if (await context.Doctors.AnyAsync(d => d.FullName == doctor.FullName))
            return Conflict($"Doctor with name '{doctor.FullName}' already exists.");

        context.Doctors.Add(doctor);
        await context.SaveChangesAsync();

        var dto = new DoctorDto
        {
            Id = doctor.Id,
            FullName = doctor.FullName,
            Age = doctor.Age,
            Experience = doctor.Experience,
            Specialty = doctor.Specialty,
            Patients = []
        };

        return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, dto);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Doctor updatedDoctor)
    {
        if (id != updatedDoctor.Id) return BadRequest();
        context.Entry(updatedDoctor).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var doctor = await context.Doctors.FindAsync(id);
        if (doctor == null) return NotFound();
        context.Doctors.Remove(doctor);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("by-specialty")]
    public async Task<ActionResult<List<DoctorDto>>> GetBySpecialty([FromQuery] string specialty)
    {
        var doctors = await context.Doctors
            .Include(d => d.Patients)
            .Where(d => EF.Functions.Like(d.Specialty, specialty))
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                FullName = d.FullName,
                Age = d.Age,
                Experience = d.Experience,
                Specialty = d.Specialty,
                Patients = d.Patients.Select(p => p.FullName).ToList()
            })
            .ToListAsync();

        return Ok(doctors);
    }
}
