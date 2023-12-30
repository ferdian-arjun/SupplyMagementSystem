namespace API.Dtos.Company;
using Entities;

public class UpdateCompanyDto
{
    public string Guid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telp { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string? BusinessType { get; set; }
    public string? Type { get; set; }
   
    
    public static implicit operator Company(UpdateCompanyDto updateCompanyDto)
    {
        return new Company()
        {
            Guid = updateCompanyDto.Guid,
            Name = updateCompanyDto.Name,
            Email = updateCompanyDto.Email,
            Telp = updateCompanyDto.Telp,
            Image = updateCompanyDto.Image,
            BusinessType = updateCompanyDto.BusinessType,
            Type = updateCompanyDto.Type,
            UpdatedAt = DateTime.Now
        };
    }
}