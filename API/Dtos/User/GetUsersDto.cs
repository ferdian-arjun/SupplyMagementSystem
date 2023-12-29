using API.Entities;

namespace API.Dtos;

public class GetUsersDto
{
    public string Guid { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public static explicit operator GetUsersDto(User user)
    {
        return new GetUsersDto
        {
            Guid = user.Guid,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            FullName = user.FullName,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
}