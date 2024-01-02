namespace API.Dtos.UserRole;

using Entities;

public class GetUserRoleDto
{
    public string Guid { get; set; }
    public string UserGuid { get; set; } = null!;
    public string RoleGuid { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public static explicit operator GetUserRoleDto(UserRole userRole)
    {
        return new GetUserRoleDto
        {
            Guid = userRole.Guid,
            UserGuid = userRole.UserGuid,
            RoleGuid = userRole.RoleGuid,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}