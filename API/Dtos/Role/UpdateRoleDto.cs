namespace API.Dtos.Role;

using Entities;

public class UpdateRoleDto
{
    public string guid { get; set; } = null!;
    public string Name { get; set; } = null!;
    
    public static implicit operator Role(UpdateRoleDto updateRoleDto)
    {
        return new Role
        {
            Guid =  updateRoleDto.guid,
            Name = updateRoleDto.Name,
            UpdatedAt = DateTime.Now
        };
    }
}