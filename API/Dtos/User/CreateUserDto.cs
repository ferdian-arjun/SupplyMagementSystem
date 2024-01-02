using API.Utilities.Handler;

namespace API.Dtos.User;
using Entities;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    
    public static implicit operator User(CreateUserDto user)
    {
        return new User()
        {
            Guid =  Guid.NewGuid().ToString(),
            Username = user.Username,
            Email = user.Email,
            Password = HashingHandler.HashPassword(user.Password),
            FullName = user.FullName,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}