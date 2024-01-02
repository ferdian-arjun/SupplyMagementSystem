namespace API.Dtos.User;
using Entities;

public class UpdateUserDto
{
    public string Guid { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    
    public static implicit operator User(UpdateUserDto user)
    {
        return new User()
        {
            Guid = user.Guid,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            UpdatedAt = DateTime.Now
        };
    }
}