namespace Backend.Models.DTOs;

public class PatientDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string WorkPlace { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }
    public List<string> Diseases { get; set; } = [];
    public string DoctorName { get; set; }
}