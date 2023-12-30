using System.Net;
using API.Dtos.ProjectVendor;
using API.Dtos.User;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("project-vendors")]
[Authorize]
[ApiController]
public class ProjectVendorController : ControllerBase
{
    private readonly ProjectVendorService _projectVendorService;

    public ProjectVendorController(ProjectVendorService projectVendorService)
    {
        _projectVendorService = projectVendorService;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var result = _projectVendorService.Get(); 
            if (!result.Any())
                return NotFound(new ResponseDataHandler<GetProjectVendorDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<IEnumerable<GetProjectVendorDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = result
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

    [HttpGet("vendor/{guid}")]
    public IActionResult GetProjectByVendor(string vendorGuid)
    {
        try
        {
            var result = _projectVendorService.GetProjectByVendor(vendorGuid);
            if (result is null)
                return NotFound(new ResponseDataHandler<IEnumerable<GetProjectByVendorDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<IEnumerable<GetProjectByVendorDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }
    
    [HttpGet("project/{guid}")]
    public IActionResult GetVendorByProject(string projectGuid)
    {
        try
        {
            var result = _projectVendorService.GetVendorByProject(projectGuid);
            if (result is null)
                return NotFound(new ResponseDataHandler<IEnumerable<GetVendorByProjectDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<IEnumerable<GetVendorByProjectDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = ex.Message
            });
        }
    }

    [HttpPut]
    public IActionResult Update(UpdateProjectVendorDto updateProjectVendorDto)
    {
        try
        {
            var update = _projectVendorService.UpdateProjectVendor(updateProjectVendorDto);
            if (update is -1)
                return NotFound(new ResponseHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id Not Found"
                });
            if (update is 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieving data from the database"
                });
            return Ok(new ResponseHandler
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.Created.ToString(),
                Message = "Successfully Updated"
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

    [HttpDelete("{projectGuid}/{vendorGuid}")]
    public IActionResult DeleteProjectVendor(string projectGuid, string vendorGuid)
    {
        try
        {
            var delete = _projectVendorService.DeleteProjectVendor(vendorGuid, projectGuid);

            if (delete is -1)
                return NotFound(new ResponseHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid not found"
                });

            if (delete is 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseExceptionHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieving data from the database"
                });

            return Ok(new ResponseHandler
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully deleted"
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

    [HttpPost]
    public IActionResult Create(CreateProjectVendorDto createProjectVendorDto)
    {
        try
        {
            var create = _projectVendorService.CreateProjectVendor(createProjectVendorDto);
            if (create is null)
                return BadRequest(new ResponseDataHandler<GetProjectVendorDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "data failed inputted"
                });

            return Ok(new ResponseDataHandler<GetProjectVendorDto>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data created successfully",
                Data = create
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