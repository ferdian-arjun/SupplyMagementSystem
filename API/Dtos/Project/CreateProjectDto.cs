namespace API.Dtos.Project;

using Entities;
using API.Utilities.Enum;

public class CreateProjectDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    
    public static implicit operator Project(CreateProjectDto project)
    {
        return new Project()
        {
            Guid =  Guid.NewGuid().ToString(),
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
    
}