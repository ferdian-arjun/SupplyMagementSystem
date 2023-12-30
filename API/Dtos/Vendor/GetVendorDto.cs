namespace API.Dtos.Vendor;

using Entities;

public class GetVendorDto
{
    public string Guid { get; set; } = null!;
    public string CompanyGuid { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string CompanyEmail { get; set; } = null!;
    public string CompanyTelp { get; set; } = null!;
    public string CompanyImage { get; set; } = null!;
    public string? CompanyBusinessType { get; set; }
    public string? CompanyType { get; set; }
    public string Status { get; set; } = null!;
    public string? ConfirmBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}