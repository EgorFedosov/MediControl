namespace Backend.Models.DTOs;

public class DiseaseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}