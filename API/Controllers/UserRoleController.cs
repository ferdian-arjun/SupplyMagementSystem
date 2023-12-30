
using System.Net;
using API.Dtos.UserRole;
using API.Dtos.Vendor;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("user-roles")]
[Authorize]
[ApiController]
public class UserRoleController: ControllerBase
{
    private readonly UserRoleService _userRoleService;

    public UserRoleController(UserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }
    
    [HttpGet("user/{userGuid}")]
    public IActionResult GetRoleByUser(string userGuid)
    {
        try
        {
            var result = _userRoleService.GetRoleByUser(userGuid); 
            if (!result.Any())
                return NotFound(new ResponseDataHandler<IEnumerable<GetUserRoleDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<IEnumerable<GetRoleByUserDto>>
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
    
    [HttpGet("role/{roleGuid}")]
    public IActionResult GetUserByRole(string roleGuid)
    {
        try
        {
            var result = _userRoleService.GetUserByRole(roleGuid); 
            if (!result.Any())
                return NotFound(new ResponseDataHandler<IEnumerable<GetUserRoleDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<IEnumerable<GetUserByRoleDto>>
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

    [HttpPost]
    public IActionResult CreateUserRole(CreateUserRoleDto createUserRoleDto)
    {
        try
        {
            var create = _userRoleService.CreateUserRole(createUserRoleDto);
            if (create is null)
                return BadRequest(new ResponseDataHandler<GetUserRoleDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "data failed inputted"
                });

            return Ok(new ResponseDataHandler<GetUserRoleDto>
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
    
    [HttpDelete()]
    public IActionResult Remove(RemoveRelationUserRoleDto removeRelationUserRoleDto)
    {
        try
        {
            var delete = _userRoleService.RemoveRelationUserRole(removeRelationUserRoleDto);

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
                Message = "Successfully remove"
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