using System.Data;
using API.Dtos.Vendor;
using Microsoft.AspNetCore.Mvc;
using SupplyManagementSystem.Repositories;

namespace SupplyManagementSystem.Controllers;

[Route("[controller]")]
public class VendorController : Controller
{
    private readonly VendorRepository _vendorRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public VendorController(VendorRepository vendorRepository, IHttpContextAccessor httpContextAccessor)
    {
        _vendorRepository = vendorRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("get")]
    public async Task<JsonResult> GetAll()
    {
        var result = await _vendorRepository.GetAll();
        return Json(result);
    }
    
    [HttpPut("update-status")]
    public async Task<JsonResult> UpdateStatus(UpdateStatusVendorDto updateStatusVendorDto)
    {
        //get userGuid
        updateStatusVendorDto.UserValidatorGuid = _httpContextAccessor.HttpContext?.Session.GetString("LogGuid");
        
        var result = await _vendorRepository.UpdateStatus(updateStatusVendorDto);
        return Json(result);
    }
}