using Backend.Models.DTOs;
using Backend.Models.Entities;

namespace Backend.Services.Interfaces;

public interface IDiseaseService
{
    Task<List<DiseaseDto>> GetAllAsync();
    Task<DiseaseDto?> GetByIdAsync(Guid id);
    Task<DiseaseDto> CreateAsync(Disease disease);
    Task<bool> UpdateAsync(Guid id, Disease disease);
    Task<bool> DeleteAsync(Guid id);
}