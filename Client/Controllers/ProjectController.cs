using API.Dtos.Project;
using API.Dtos.ProjectVendor;
using Microsoft.AspNetCore.Mvc;
using SupplyManagementSystem.Repositories;

namespace SupplyManagementSystem.Controllers;

[Route("[controller]")]
public class ProjectController : Controller
{
    private readonly ProjectRepository _projectRepository;
    private readonly ProjectVendorRepository _projectVendorRepository;

    public ProjectController(ProjectRepository projectRepository, ProjectVendorRepository projectVendorRepository)
    {
        _projectRepository = projectRepository;
        _projectVendorRepository = projectVendorRepository;
    }
    
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("get")]
    public async Task<JsonResult> GetAll()
    {
        var result = await _projectRepository.GetAll();
        return Json(result);
    }
    
    [HttpPost("post")]
    public async Task<JsonResult>  Post(CreateProjectDto createProjectDto)
    {
        var result = await _projectRepository.Post(createProjectDto);
        return Json(result);
    }
    
    [HttpGet("get/{guid}")]
    public async Task<JsonResult> Get(string guid)
    {
        var result = await _projectRepository.Get(guid);
        return Json(result);
    }
    
    [HttpPut("update")]
    public async Task<JsonResult> Put(UpdateProjectDto updateProjectDto)
    {
        var result = await _projectRepository.Put(updateProjectDto);
        return Json(result);
    }
    
    [HttpDelete("deleted/{guid}")]
    public async Task<JsonResult> Delete(string guid)
    {
        var result = await _projectRepository.Delete(guid);
        return Json(result);
    }
    
    [HttpPost("add-vendor")]
    public async Task<JsonResult>  AddVendor(CreateProjectVendorDto createProjectVendorDto)
    {
        var result = await _projectVendorRepository.Post(createProjectVendorDto);
        return Json(result);
    }
}