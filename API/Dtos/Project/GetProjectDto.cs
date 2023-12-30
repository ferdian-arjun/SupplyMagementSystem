namespace API.Dtos.Project;

using Entities;

public class GetProjectDto
{
    public string Guid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public static explicit operator GetProjectDto(Project project)
    {
        return new GetProjectDto()
        {
            Guid =  project.Guid,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status.ToString(),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}