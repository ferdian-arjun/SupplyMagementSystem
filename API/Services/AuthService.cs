using API.Dtos.Login;
using API.Interface;
using API.Utilities.Handler;

namespace API.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
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

            var token = _jwtService.GenerateToken(existingUser);
            
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