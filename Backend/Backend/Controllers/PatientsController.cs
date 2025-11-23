using Backend.Models.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPatientService patientService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PatientDto>>> GetAll() => 
        Ok(await patientService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PatientDto>> GetById(Guid id)
    {
        var patient = await patientService.GetByIdAsync(id);
        return patient is null ? NotFound() : Ok(patient);
    }

    [HttpGet("by-name")]
    public async Task<ActionResult<List<PatientDto>>> GetByName([FromQuery] string name) => 
        Ok(await patientService.GetByNameAsync(name));

    [HttpPost]
    public async Task<ActionResult<PatientDto>> Create(CreatePatientDto dto)
    {
        var created = await patientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdatePatientDto dto)
    {
        var result = await patientService.UpdateAsync(id, dto);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await patientService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}