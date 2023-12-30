using API.Utilities.Enum;

namespace API.Dtos.Project;

using Entities;

public class UpdateProjectDto
{
    public string Guid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    
    public static implicit operator Project(UpdateProjectDto project)
    {
        return new Project()
        {
            Guid =  project.Guid,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status,
            UpdatedAt = DateTime.Now
        };
    }
}