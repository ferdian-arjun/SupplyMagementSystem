using API.Dtos.User;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using SupplyManagementSystem.Repositories;

namespace SupplyManagementSystem.Controllers;

[Route("[controller]")]
public class UserController : Controller
{
    private readonly UserRepository _userRepository;
    public UserController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("get")]
    public async Task<JsonResult> GetAll()
    {
        var result = await _userRepository.GetAll();
        return Json(result);
    }
    
    [HttpPost("post")]
    public async Task<JsonResult>  Post(CreateUserDto createUserDto)
    {
        var result = await _userRepository.Post(createUserDto);
        return Json(result);
    }
    
    [HttpGet("get/{guid}")]
    public async Task<JsonResult> Get(string guid)
    {
        var result = await _userRepository.Get(guid);
        return Json(result);
    }
    
    [HttpPut("update")]
    public async Task<JsonResult> Put(UpdateUserDto updateUserDto)
    {
        var result = await _userRepository.Put(updateUserDto);
        return Json(result);
    }
    
    [HttpDelete("deleted/{guid}")]
    public async Task<JsonResult> Delete(string guid)
    {
        var result = await _userRepository.Delete(guid);
        return Json(result);
    }

}