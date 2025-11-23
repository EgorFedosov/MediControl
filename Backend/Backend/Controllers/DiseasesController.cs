using Backend.Models.DTOs;
using Backend.Models.Entities;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiseasesController(IDiseaseService diseaseService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DiseaseDto>>> GetAll()
    {
        return Ok(await diseaseService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DiseaseDto>> GetById(Guid id)
    {
        var disease = await diseaseService.GetByIdAsync(id);
        return disease is null ? NotFound() : Ok(disease);
    }

    [HttpPost]
    public async Task<ActionResult<DiseaseDto>> Create(Disease disease)
    {
        var created = await diseaseService.CreateAsync(disease);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Disease updatedDisease)
    {
        if (id != updatedDisease.Id) return BadRequest();
        var result = await diseaseService.UpdateAsync(id, updatedDisease);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await diseaseService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}