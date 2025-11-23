using Backend.Models.DTOs;
using Backend.Models.Entities;

namespace Backend.Services.Interfaces;

public interface IDoctorService
{
    Task<List<DoctorDto>> GetAllAsync();
    Task<DoctorDto?> GetByIdAsync(Guid id);
    Task<List<DoctorDto>> GetBySpecialtyAsync(string specialty);
    Task<DoctorDto> CreateAsync(Doctor doctor);
    Task<bool> UpdateAsync(Guid id, Doctor doctor);
    Task<bool> DeleteAsync(Guid id);
}