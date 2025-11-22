using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Entities;

public class Disease
{
    public Guid Id { get; init; } = Guid.NewGuid();
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;
    [MaxLength(500)]
    public string Description { get; init; } = string.Empty;

    public List<Patient> Patients { get; init; } = [];
}