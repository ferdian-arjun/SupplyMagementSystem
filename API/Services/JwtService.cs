using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using API.Entities;
using API.Interface;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user, List<string> roleUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
    
        var claims = new List<Claim>
        {
            new Claim("FullName", user.FullName),
            new Claim("Guid", user.Guid),
            new Claim(ClaimTypes.Email, user.Email),
        };

        foreach (var role in roleUser)
        {
            claims.Add(new Claim("Role", role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}