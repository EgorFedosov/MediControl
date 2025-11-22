using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Entities;

public class Doctor
{
    public Guid Id { get; init; } = Guid.NewGuid();
    [MaxLength(100)]
    public string FullName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public int Experience { get; init; }
    [MaxLength(100)]
    public string Specialty { get; init; } = string.Empty;
    public List<Patient> Patients { get; init; } = [];
}