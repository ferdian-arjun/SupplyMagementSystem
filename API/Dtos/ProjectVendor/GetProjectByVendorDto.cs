namespace API.Dtos.ProjectVendor;

public class GetProjectByVendorDto
{
    public string ProjectGuid { get; set; } = null!;
    public string VendorGuid { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public DateTime ProjectStartDate { get; set; }
    public DateTime ProjectEndDate { get; set; }
    public string ProjectStatus { get; set; } = null!;
    
    public string VendorName { get; set; } = null!;
    public string VendorEmail { get; set; } = null!;
    public string VendorTelp { get; set; } = null!;
    public string VendorImage { get; set; } = null!;
    public string? VendorBusinessType { get; set; }
    public string? VendorType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}