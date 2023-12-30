using System.Net;
using API.Dtos;
using API.Dtos.Vendor;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("vendors")]
[Authorize]
[ApiController]
public class VendorController : ControllerBase
{
    private readonly VendorService _userService;

    public VendorController(VendorService userService)
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
                return NotFound(new ResponseDataHandler<GetVendorDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<IEnumerable<GetVendorDto>>
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
                return NotFound(new ResponseDataHandler<GetVendorDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            return Ok(new ResponseDataHandler<GetVendorDto>
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

    [HttpPut("update-status")]
    public IActionResult UpdateStatus(UpdateStatusVendorDto updateStatusVendorDto)
    {
        try
        {
            var update = _userService.UpdateStatus(updateStatusVendorDto);
            return update switch
            {
                -2 => NotFound(new ResponseHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "User data Not Found"
                }),
                -1 => NotFound(new ResponseHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                }),
                0 => StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseExceptionHandler
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Status = HttpStatusCode.InternalServerError.ToString(),
                        Message = "Error retrieving data from the database"
                    }),
                _ => Ok(new ResponseHandler
                {
                    Code = StatusCodes.Status201Created,
                    Status = HttpStatusCode.Created.ToString(),
                    Message = "Successfully Updated"
                })
            };
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