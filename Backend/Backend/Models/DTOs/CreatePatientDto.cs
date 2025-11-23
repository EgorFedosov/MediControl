using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTOs;

public class CreatePatientDto
{
    [StringLength(100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string WorkPlace { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    [Required]
    public Guid DoctorId { get; set; }

    public List<string> Diseases { get; set; } = [];
}