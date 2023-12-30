namespace API.Dtos.UserRole;

public class GetUserByRoleDto
{
    public string UserGuid { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string RoleGuid { get; set; }
    public string RoleName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}