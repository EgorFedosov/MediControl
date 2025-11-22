using Backend.Data;
using Backend.Models.DTOs;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiseasesController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DiseaseDto>>> GetAll()
    {
        var diseases = await context.Diseases
            .Select(d => new DiseaseDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description
            })
            .ToListAsync();
        return Ok(diseases);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DiseaseDto>> GetById(Guid id)
    {
        var disease = await context.Diseases
            .Where(d => d.Id == id)
            .Select(d => new DiseaseDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description
            })
            .FirstOrDefaultAsync();

        return disease is null ? NotFound() : Ok(disease);
    }

    [HttpPost]
    public async Task<ActionResult<DiseaseDto>> Create(Disease disease)
    {
        if (await context.Diseases.AnyAsync(d => d.Name == disease.Name))
            return Conflict($"Disease with name '{disease.Name}' already exists.");

        context.Diseases.Add(disease);
        await context.SaveChangesAsync();

        var dto = new DiseaseDto
        {
            Id = disease.Id,
            Name = disease.Name,
            Description = disease.Description
        };

        return CreatedAtAction(nameof(GetById), new { id = disease.Id }, dto);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, Disease updatedDisease)
    {
        if (id != updatedDisease.Id) return BadRequest();
        context.Entry(updatedDisease).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var disease = await context.Diseases.FindAsync(id);
        if (disease == null) return NotFound();
        context.Diseases.Remove(disease);
        await context.SaveChangesAsync();
        return NoContent();
    }
}