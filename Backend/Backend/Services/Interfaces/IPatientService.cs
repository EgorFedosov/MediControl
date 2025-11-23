using Backend.Models.DTOs;

namespace Backend.Services.Interfaces;

public interface IPatientService
{
    Task<List<PatientDto>> GetAllAsync();
    Task<PatientDto?> GetByIdAsync(Guid id);
    Task<List<PatientDto>> GetByNameAsync(string name);
    Task<PatientDto> CreateAsync(CreatePatientDto dto);
    Task<bool> UpdateAsync(Guid id, UpdatePatientDto dto);
    Task<bool> DeleteAsync(Guid id);
}