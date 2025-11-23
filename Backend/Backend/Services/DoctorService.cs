using Backend.Data;
using Backend.Models.DTOs;
using Backend.Models.Entities;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class DoctorService(ApplicationContext context) : IDoctorService
{
    public async Task<List<DoctorDto>> GetAllAsync()
    {
        return await context.Doctors
            .Include(d => d.Patients)
            .Select(d => MapToDto(d))
            .ToListAsync();
    }

    public async Task<DoctorDto?> GetByIdAsync(Guid id)
    {
        var doctor = await context.Doctors
            .Include(d => d.Patients)
            .FirstOrDefaultAsync(d => d.Id == id);
            
        return doctor == null ? null : MapToDto(doctor);
    }

    public async Task<List<DoctorDto>> GetBySpecialtyAsync(string specialty)
    {
        return await context.Doctors
            .Include(d => d.Patients)
            .Where(d => EF.Functions.Like(d.Specialty, specialty))
            .Select(d => MapToDto(d))
            .ToListAsync();
    }

    public async Task<DoctorDto> CreateAsync(Doctor doctor)
    {
        context.Doctors.Add(doctor);
        await context.SaveChangesAsync();
        return MapToDto(doctor);
    }

    public async Task<bool> UpdateAsync(Guid id, Doctor updatedDoctor)
    {
        var doctor = await context.Doctors.FindAsync(id);
        if (doctor == null) return false;

        context.Entry(doctor).CurrentValues.SetValues(updatedDoctor);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var doctor = await context.Doctors.FindAsync(id);
        if (doctor == null) return false;

        context.Doctors.Remove(doctor);
        await context.SaveChangesAsync();
        return true;
    }

    private static DoctorDto MapToDto(Doctor d) => new()
    {
        Id = d.Id,
        FullName = d.FullName,
        Age = d.Age,
        Experience = d.Experience,
        Specialty = d.Specialty,
        Patients = d.Patients.Select(p => p.FullName).ToList()
    };
}