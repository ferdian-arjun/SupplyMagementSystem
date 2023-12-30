namespace API.Dtos.Role;
using Entities;

public class CreateRoleDto
{
    public string Name { get; set; } = null!;
    
    public static implicit operator Role(CreateRoleDto createRoleDto)
    {
        return new Role
        {
            Guid =  Guid.NewGuid().ToString(),
            Name = createRoleDto.Name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}