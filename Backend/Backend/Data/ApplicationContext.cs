using Microsoft.EntityFrameworkCore;
using Backend.Models.Entities;

namespace Backend.Data;

public sealed class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Disease> Diseases { get; set; } = null!;
}