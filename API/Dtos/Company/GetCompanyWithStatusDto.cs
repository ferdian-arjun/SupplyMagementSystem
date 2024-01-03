namespace API.Dtos.Company;

public class GetCompanyWithStatusDto
{
    public string Guid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telp { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string? BusinessType { get; set; }
    public string? Type { get; set; }
    public string Status { get; set; }
    public string ConfirmBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}