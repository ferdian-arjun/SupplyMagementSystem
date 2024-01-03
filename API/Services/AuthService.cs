using API.Dtos.Login;
using API.Entities;
using API.Interface;
using API.Utilities.Handler;

namespace API.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService, IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _userRoleRepository = userRoleRepository;
    }

    public ResponseServiceHandler<TokenDto> Login(LoginDto loginDto)
    {
        try
        {
            var existingUser = _userRepository.Get(where: user => user.Email.Equals(loginDto.Email)).FirstOrDefault();

            if (existingUser == null || !HashingHandler.ValidatePassword(loginDto.Password, existingUser.Password))
            {
                return new ResponseServiceHandler<TokenDto>
                {
                    Code = 401,
                    Message = "Invalid email or password"
                };
            }
            
            //get role user
            var roles = _userRoleRepository.Get(where: ur => ur.UserGuid.Equals(existingUser.Guid),
                includes: ur => ur.Role!);

            var roleUser = new List<string>();
            foreach (var role in roles)
            {
                roleUser.Add(role.Role.Name);
            }
           
            var token = _jwtService.GenerateToken(existingUser, roleUser);
            
            return new ResponseServiceHandler<TokenDto>
            {
                Code = 200,
                Message = "Data Success",
                Data = new TokenDto()
                {
                    Token = token
                }
            };
        }
        catch (Exception e)
        {
            return new ResponseServiceHandler<TokenDto>()
            {
                Code = 500,
                Message = e.Message
            };
        }
       
    }
}