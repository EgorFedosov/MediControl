using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Entities;

public class Patient
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string WorkPlace { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    [NotMapped]
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
    public Guid DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public List<Disease> Diseases { get; set; } = [];
}