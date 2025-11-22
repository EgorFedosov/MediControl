using Backend.Data;
using Backend.Models.DTOs;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PatientDto>>> GetAll()
    {
        var patients = await context.Patients
            .Include(p => p.Doctor)
            .Include(p => p.Diseases)
            .Select(p => new PatientDto
            {
                Id = p.Id,
                FullName = p.FullName,
                WorkPlace = p.WorkPlace,
                Address = p.Address,
                Age = p.Age,
                DoctorName = p.Doctor != null ? p.Doctor.FullName : "Not assigned",
                Diseases = p.Diseases.Select(d => d.Name).ToList()
            })
            .ToListAsync();

        return Ok(patients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PatientDto>> GetById(Guid id)
    {
        var patient = await context.Patients
            .Include(p => p.Doctor)
            .Include(p => p.Diseases)
            .Where(p => p.Id == id)
            .Select(p => new PatientDto
            {
                Id = p.Id,
                FullName = p.FullName,
                WorkPlace = p.WorkPlace,
                Address = p.Address,
                Age = p.Age,
                DoctorName = p.Doctor != null ? p.Doctor.FullName : "Not assigned",
                Diseases = p.Diseases.Select(d => d.Name).ToList()
            })
            .FirstOrDefaultAsync();

        return patient is null ? NotFound() : Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> Create(Patient patient)
    {
        patient.Doctor = await context.Doctors.FindAsync(patient.DoctorId);
        if (patient.Doctor == null)
            return BadRequest("Doctor not found");

        if (patient.Diseases.Count > 0)
        {
            var inputDiseaseIds = patient.Diseases.Where(d => d.Id != Guid.Empty).Select(d => d.Id).ToList();
            var existingDiseases = await context.Diseases
                .Where(d => inputDiseaseIds.Contains(d.Id))
                .ToListAsync();

            var newDiseases = patient.Diseases
                .Where(d => d.Id == Guid.Empty || existingDiseases.All(ed => ed.Id != d.Id))
                .Select(d => new Disease
                {
                    Id = d.Id == Guid.Empty ? Guid.NewGuid() : d.Id,
                    Name = d.Name,
                    Description = d.Description
                })
                .ToList();

            patient.Diseases = existingDiseases.Concat(newDiseases).ToList();
        }

        context.Patients.Add(patient);
        await context.SaveChangesAsync();


        var dto = new PatientDto
        {
            Id = patient.Id,
            FullName = patient.FullName,
            WorkPlace = patient.WorkPlace,
            Address = patient.Address,
            Age = patient.Age,
            DoctorName = patient.Doctor?.FullName ?? "Not assigned",
            Diseases = patient.Diseases.Select(d => d.Name).ToList()
        };

        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, dto);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Patient updatedPatient)
    {
        if (id != updatedPatient.Id) return BadRequest();
        context.Entry(updatedPatient).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var patient = await context.Patients.FindAsync(id);
        if (patient == null) return NotFound();
        context.Patients.Remove(patient);
        await context.SaveChangesAsync();
        return NoContent();
    }
}