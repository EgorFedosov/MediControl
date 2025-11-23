using Backend.Data;
using Backend.Models.DTOs;
using Backend.Models.Entities;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class DiseaseService(ApplicationContext context) : IDiseaseService
{
    public async Task<List<DiseaseDto>> GetAllAsync()
    {
        return await context.Diseases
            .Select(d => MapToDto(d))
            .ToListAsync();
    }

    public async Task<DiseaseDto?> GetByIdAsync(Guid id)
    {
        var disease = await context.Diseases.FindAsync(id);
        return disease == null ? null : MapToDto(disease);
    }

    public async Task<DiseaseDto> CreateAsync(Disease disease)
    {
        context.Diseases.Add(disease);
        await context.SaveChangesAsync();
        return MapToDto(disease);
    }

    public async Task<bool> UpdateAsync(Guid id, Disease updatedDisease)
    {
        var disease = await context.Diseases.FindAsync(id);
        if (disease == null) return false;

        context.Entry(disease).CurrentValues.SetValues(updatedDisease);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var disease = await context.Diseases.FindAsync(id);
        if (disease == null) return false;

        context.Diseases.Remove(disease);
        await context.SaveChangesAsync();
        return true;
    }

    private static DiseaseDto MapToDto(Disease d) => new()
    {
        Id = d.Id,
        Code = d.Code,
        Name = d.Name,
        Description = d.Description
    };
}