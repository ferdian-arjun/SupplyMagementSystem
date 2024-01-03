using API.Utilities.Enum;

namespace API.Dtos.Company;
using Entities;

public class GetCompanyDto
{
    public string Guid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telp { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string? BusinessType { get; set; }
    public string? Type { get; set; }
    public bool? IsApproved { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public static explicit operator GetCompanyDto(Company company)
    {
        return new GetCompanyDto()
        {
            Guid = company.Guid,
            Name = company.Name,
            Email = company.Email,
            Telp = company.Telp,
            Image = company.Image,
            BusinessType = company.BusinessType,
            Type = company.Type,
            IsApproved =  company.TblTrVendors.FirstOrDefault().Status == VendorStatus.Approval,
            CreatedAt = company.CreatedAt,
            UpdatedAt = company.UpdatedAt
        };
    }
}