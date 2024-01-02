using API.Dtos.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SupplyManagementSystem.Repositories;
using SupplyManagementSystem.Utilities.Handler;

namespace SupplyManagementSystem.Controllers;

public class AuthController : Controller
{
    private readonly AuthRepository _authRepository;

    public AuthController(AuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost("check-login")]
    public async Task<IActionResult> CheckLogin(LoginDto loginDto)
    {
        if (loginDto.Email.IsNullOrEmpty() || loginDto.Password.IsNullOrEmpty()) {
            TempData["Message"] = "Email or username and password cannot be empty";
            return RedirectToAction("login");
        }

        var jwtToken = await _authRepository.Auth(loginDto);
        var token = jwtToken.Data?.Token;
        var message = jwtToken.Message;
        if (jwtToken.Data == null)
        {
            TempData["Message"] = message;
            return RedirectToAction("login");
        }

        //Reading claims
        string getEmail = JwtHandler.GetClaim(token, JwtRegisteredClaimNames.Email);
        string getFullName = JwtHandler.GetClaim(token, "FullName");
        string getGuid = JwtHandler.GetClaim(token, "Guid");
            
        //set session
        HttpContext.Session.SetString("JWToken", token);
        HttpContext.Session.SetString("LogEmail", getEmail);
        HttpContext.Session.SetString("LogGuid", getGuid);
        HttpContext.Session.SetString("LogFullName", getFullName);
          
        return RedirectToAction("index", "Home");
    }
}