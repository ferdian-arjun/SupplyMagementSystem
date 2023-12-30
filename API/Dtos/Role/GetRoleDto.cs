namespace API.Dtos.Role;

public class GetRoleDto
{
    public string Guid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public static explicit operator GetRoleDto(Entities.Role role)
    {
        return new GetRoleDto
        {
            Guid = role.Guid,
            Name = role.Name,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt
        };
    }
}