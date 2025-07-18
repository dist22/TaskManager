using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/auth")]

public class UserAuthController(IUserAuthService userAuthService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsyncController([FromForm]UserCreateDto userCreateDto)
    {
        await userAuthService.RegisterUserAsync(userCreateDto);
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsyncController([FromForm]UserLoginDto userLoginDto) 
        => Ok(await userAuthService.LoginUserAsync(userLoginDto));
}