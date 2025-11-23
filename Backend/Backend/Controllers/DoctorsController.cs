using Backend.Models.DTOs;
using Backend.Models.Entities;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(IDoctorService doctorService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DoctorDto>>> GetAll()
    {
        return Ok(await doctorService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DoctorDto>> GetById(Guid id)
    {
        var doctor = await doctorService.GetByIdAsync(id);
        return doctor is null ? NotFound() : Ok(doctor);
    }

    [HttpGet("by-specialty")]
    public async Task<ActionResult<List<DoctorDto>>> GetBySpecialty([FromQuery] string specialty)
    {
        return Ok(await doctorService.GetBySpecialtyAsync(specialty));
    }

    [HttpPost]
    public async Task<ActionResult<DoctorDto>> Create(Doctor doctor)
    {
        var created = await doctorService.CreateAsync(doctor);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Doctor updatedDoctor)
    {
        if (id != updatedDoctor.Id) return BadRequest();
        var result = await doctorService.UpdateAsync(id, updatedDoctor);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await doctorService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}