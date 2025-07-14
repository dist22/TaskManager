using TaskManager.Domain.Models;
using TaskManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Domain.Enums;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserServices userServices) : ControllerBase
{
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetAsyncController(int id)
        => Ok(await userServices.GetAsync<UserDto>(id));

    [HttpGet("all")]
    public async Task<IEnumerable<UserDto>> GetAllAsyncController() 
        => await userServices.GetAllAsync<UserDto>();
    
    [HttpGet("active")]
    public async Task<IEnumerable<UserDto>> GetAllActiveAsyncController()
        => await userServices.GetAllActiveAsync<UserDto>();

    [HttpPost("user/post")]
    public async Task<IActionResult> CreateUserAsyncController([FromForm] UserCreateDto user)
    {
        await userServices.CreateAsync(user);
        return Ok();
    }

    [HttpPut("user-edit/{id}")]
    public async Task<IActionResult> EditUserAsyncController([FromForm] UserEditDto userEditDto, int id)
    {
        await userServices.UpdateAsync(userEditDto, id);
        return Ok();
    }

    [HttpPatch("user/{id}/change-status")]
    public async Task<IActionResult> ChangeStatusAsyncController(int id, ActiveStatus activeStatus)
    {
        await userServices.ChangeStatusAsync(id, activeStatus);
        return Ok();
    }

    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteUserAsyncController(int id)
    {
        await userServices.DeleteAsync(id);
        return Ok();
    }

}