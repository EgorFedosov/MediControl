namespace Backend.Models.DTOs;

public class DoctorDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public int Experience { get; set; }
    public string Specialty { get; set; }
    public List<string> Patients { get; set; } = [];
}
