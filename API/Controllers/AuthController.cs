using System.Net;
using API.Dtos;
using API.Dtos.Login;
using API.Entities;
using API.Interface;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("login")]
    public IActionResult Login(LoginDto loginDto)
    {
        try
        {
            var result = _authService.Login(loginDto);

            if (result.Code == 200) return Ok(new ResponseDataHandler<TokenDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = result.Message,
                Data = result.Data
            });
            
            return Unauthorized(new ResponseDataHandler<TokenDto>()
            {
                Code = StatusCodes.Status401Unauthorized,
                Status = HttpStatusCode.Unauthorized.ToString(),
                Message = result.Message
            });
            
        }
        catch (Exception ex)
        {
            //if error
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }
}