namespace API.Dtos.Company;
using Entities;

public class CreateCompanyDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telp { get; set; } = null!;
    public string Image { get; set; } = null!;
   
    
    public static implicit operator Company(CreateCompanyDto createCompanyDto)
    {
        return new Company()
        {
            Guid = Guid.NewGuid().ToString(),
            Name = createCompanyDto.Name,
            Email = createCompanyDto.Email,
            Telp = createCompanyDto.Telp,
            Image = createCompanyDto.Image,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}