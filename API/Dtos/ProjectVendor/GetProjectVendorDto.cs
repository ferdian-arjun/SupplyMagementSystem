namespace API.Dtos.ProjectVendor;

using Entities;
public class GetProjectVendorDto
{
    public string ProjectGuid { get; set; } = null!;
    public string VendorGuid { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public static explicit operator GetProjectVendorDto(ProjectVendor projectVendor)
    {
        return new GetProjectVendorDto
        {
            ProjectGuid = projectVendor.ProjectGuid,
            VendorGuid = projectVendor.VendorGuid,
            CreatedAt = projectVendor.CreatedAt,
            UpdatedAt = projectVendor.UpdatedAt,
        };
    }
}