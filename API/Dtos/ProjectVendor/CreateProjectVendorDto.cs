namespace API.Dtos.ProjectVendor;
using Entities;
public class CreateProjectVendorDto
{
    public string ProjectGuid { get; set; } = null!;
    public string VendorGuid { get; set; } = null!;
    
    public static implicit operator ProjectVendor(CreateProjectVendorDto projectVendorDto)
    {
        return new ProjectVendor()
        {
            Guid =  Guid.NewGuid().ToString(),
            ProjectGuid  =  projectVendorDto.ProjectGuid,
            VendorGuid = projectVendorDto.VendorGuid,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}