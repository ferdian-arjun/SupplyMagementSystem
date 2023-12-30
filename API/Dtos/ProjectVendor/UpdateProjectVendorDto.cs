namespace API.Dtos.ProjectVendor;

using Entities;

public class UpdateProjectVendorDto
{
    public string ProjectGuid { get; set; } = null!;
    public string VendorGuid { get; set; } = null!;
    
    public static implicit operator ProjectVendor(UpdateProjectVendorDto updateProjectVendorDto)
    {
        return new ProjectVendor()
        {
            ProjectGuid =  updateProjectVendorDto.ProjectGuid,
            VendorGuid = updateProjectVendorDto.VendorGuid,
            UpdatedAt = DateTime.Now
        };
    }
}