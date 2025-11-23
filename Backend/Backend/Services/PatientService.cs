using Backend.Data;
using Backend.Models.DTOs;
using Backend.Models.Entities;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class PatientService(ApplicationContext context) : IPatientService
{
    public async Task<List<PatientDto>> GetAllAsync()
    {
        return await context.Patients
            .AsNoTracking()
            .Include(p => p.Doctor)
            .Include(p => p.Diseases)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<PatientDto?> GetByIdAsync(Guid id)
    {
        var patient = await context.Patients
            .AsNoTracking()
            .Include(p => p.Doctor)
            .Include(p => p.Diseases)
            .FirstOrDefaultAsync(p => p.Id == id);

        return patient == null ? null : MapToDto(patient);
    }

    public async Task<List<PatientDto>> GetByNameAsync(string name)
    {
        return await context.Patients
            .AsNoTracking()
            .Include(p => p.Doctor)
            .Include(p => p.Diseases)
            .Where(p => EF.Functions.ILike(p.FullName, $"%{name}%"))
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
    {
        var patient = new Patient
        {
            FullName = dto.FullName,
            WorkPlace = dto.WorkPlace,
            Address = dto.Address,
            DateOfBirth = dto.DateOfBirth,
            DoctorId = dto.DoctorId
        };

        if (dto.Diseases.Count > 0)
        {
            var diseases = await context.Diseases
                .Where(d => dto.Diseases.Contains(d.Name))
                .ToListAsync();
            patient.Diseases = diseases;
        }

        context.Patients.Add(patient);
        await context.SaveChangesAsync();
        
        await context.Entry(patient).Reference(p => p.Doctor).LoadAsync();
        return MapToDto(patient);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePatientDto dto)
    {
        var existingPatient = await context.Patients
            .Include(p => p.Diseases)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingPatient == null) return false;

        existingPatient.FullName = dto.FullName;
        existingPatient.WorkPlace = dto.WorkPlace;
        existingPatient.Address = dto.Address;
        existingPatient.DateOfBirth = dto.DateOfBirth;
        existingPatient.DoctorId = dto.DoctorId;

        var currentDiseases = existingPatient.Diseases.Select(d => d.Name).ToHashSet();
        var newDiseases = dto.Diseases.ToHashSet();

        if (!currentDiseases.SetEquals(newDiseases))
        {
            var diseasesToAdd = await context.Diseases
               .Where(d => newDiseases.Contains(d.Name))
                .ToListAsync();
            
            existingPatient.Diseases = diseasesToAdd;
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var rowsAffected = await context.Patients
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return rowsAffected > 0;
    }

    private static PatientDto MapToDto(Patient p) => new()
    {
        Id = p.Id,
        FullName = p.FullName,
        WorkPlace = p.WorkPlace,
        Address = p.Address,
        DateOfBirth = p.DateOfBirth,
        Age = p.Age,
        DoctorId = p.DoctorId,
        DoctorName = p.Doctor?.FullName ?? "Не назначен",
        Diseases = p.Diseases.Select(d => d.Name).ToList()
    };
}