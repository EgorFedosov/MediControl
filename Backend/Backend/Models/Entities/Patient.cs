namespace Backend.Models.Entities;

public class Patient
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string WorkPlace { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int BirthYear { get; set; }
    public int Age { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public List<Disease> Diseases { get; set; } = [];
}