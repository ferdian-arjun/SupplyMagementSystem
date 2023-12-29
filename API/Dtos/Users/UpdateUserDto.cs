using API.Entities;

namespace API.Dtos;

public class UpdateUserDto
{
    public string Guid { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    
    public static implicit operator User(UpdateUserDto user)
    {
        return new User()
        {
            Guid = user.Guid,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            FullName = user.FullName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}