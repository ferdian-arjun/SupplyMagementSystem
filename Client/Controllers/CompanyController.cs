using API.Dtos.Company;
using Microsoft.AspNetCore.Mvc;
using SupplyManagementSystem.Repositories;

namespace SupplyManagementSystem.Controllers;

[Route("[controller]")]
public class CompanyController : Controller
{
    private readonly CompanyRepository _companyRepository;
    public CompanyController(CompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("get")]
    public async Task<JsonResult> GetAll()
    {
        var result = await _companyRepository.GetAll();
        return Json(result);
    }
    
    [HttpPost("post")]
    public async Task<JsonResult>  Post(CreateCompanyDto createCompanyDto)
    {
        var result = await _companyRepository.Post(createCompanyDto);
        return Json(result);
    }
    
    [HttpGet("get/{guid}")]
    public async Task<JsonResult> Get(string guid)
    {
        var result = await _companyRepository.Get(guid);
        return Json(result);
    }
    
    [HttpPut("update")]
    public async Task<JsonResult> Put(UpdateCompanyDto updateCompanyDto)
    {
        var result = await _companyRepository.Put(updateCompanyDto);
        return Json(result);
    }
    
    [HttpDelete("deleted/{guid}")]
    public async Task<JsonResult> Delete(string guid)
    {
        var result = await _companyRepository.Delete(guid);
        return Json(result);
    }
    
    
    [HttpGet("CompanyRegister")]
    public IActionResult CompanyRegister()
    {
        return View();
    }
    
    [HttpGet("get-waiting-for-approval")]
    public async Task<JsonResult> GetWaitingForApproval()
    {
        var result = await _companyRepository.GetWaitingForApproval();
        return Json(result);
    }
}