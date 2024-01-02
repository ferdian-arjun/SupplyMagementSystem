namespace API.Dtos.User;
using Entities;

public class GetUserDto
{
    public string Guid { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public static explicit operator GetUserDto(User user)
    {
        return new GetUserDto
        {
            Guid = user.Guid,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}