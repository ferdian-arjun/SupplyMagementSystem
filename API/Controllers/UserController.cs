using System.Net;
using API.Dtos;
using API.Dtos.User;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("users")]
[Authorize]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var result = _userService.Get(); 
            if (!result.Any())
                return NotFound(new ResponseDataHandler<GetUserDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<IEnumerable<GetUserDto>>
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

    [HttpGet("{guid}")]
    public ActionResult GetByGuid(string guid)
    {
        try
        {
            var result = _userService.GetByGuid(guid);
            if (result is null)
                return NotFound(new ResponseDataHandler<GetUserDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<GetUserDto>
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
    public IActionResult Update(UpdateUserDto updateUserDto)
    {
        try
        {
            var update = _userService.UpdateUser(updateUserDto);
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
                Code = StatusCodes.Status200OK,
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

    [HttpDelete("{guid}")]
    public IActionResult Delete(string guid)
    {
        try
        {
            var delete = _userService.DeleteUser(guid);

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
    [AllowAnonymous]
    public IActionResult Create(CreateUserDto createUserDto)
    {
        try
        {
            var create = _userService.CreateUser(createUserDto);
            if (create is null)
                return BadRequest(new ResponseDataHandler<GetUserDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "data failed inputted"
                });

            return Ok(new ResponseDataHandler<GetUserDto>
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