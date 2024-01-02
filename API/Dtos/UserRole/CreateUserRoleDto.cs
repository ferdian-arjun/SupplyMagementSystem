namespace API.Dtos.UserRole;
using Entities;

public class CreateUserRoleDto
{
    public string UserGuid { get; set; } = null!;
    public string RoleGuid { get; set; } = null!;
    
    public static implicit operator UserRole(CreateUserRoleDto createRoleDto)
    {
        return new UserRole
        {
            Guid =  Guid.NewGuid().ToString(),
            UserGuid =  createRoleDto.UserGuid,
            RoleGuid = createRoleDto.RoleGuid,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }
}