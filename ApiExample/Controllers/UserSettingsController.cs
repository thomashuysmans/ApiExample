using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers;

[ApiController]
[Route("/user-settings")]
public class UserSettingsController : ControllerBase
{
    
    [HttpGet()]
    [Route("{userId}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Get([FromRoute] int userId)
    {

        if (userId == 1)
        {
            var userSettings = new UserSettings
            {
                Id = 1,
                ScreenSetting = ScreenSetting.Dark
            };

            return Ok(new ApiResponse()
            {
                Data = userSettings
            });    
        }

        return BadRequest(new ProblemDetails()
        {
            Status = 400,
            Title = "User not found",
            Detail = "User with id " + userId + " not found"
        });
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] UserSettings userSettings)
    {
        if (userSettings == null)
        {
            return BadRequest(new ProblemDetails()
            {
                Status = 400,
                Title = "Invalid request",
                Detail = "User settings are required"
            });
        }
        
        return Ok(new ApiResponse()
        {
            Data = userSettings
        });
    }
}

public record UserSettings
{
    public int Id { get; set; }
    
    public ScreenSetting ScreenSetting { get; set; } 
}

public enum ScreenSetting
{
    Light,
    Dark
}


public record ApiResponse
{
    public object Data { get; set; }
    
    
}

public record ApiErrorResponse
{
    public string Status { get; set; }
    
    public string Code { get; set; }
    
    public string Title { get; set; }
    
    public string Detail { get; set; }
}