using TaskManager.Domain.Models;
using TaskManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserServices userServices) : ControllerBase
{
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUser(int id)
        => Ok(await userServices.GetByPredicateAsync<UserDto>(u => u.Id == id));

    [HttpGet("all")]
    public async Task<IEnumerable<UserDto>> GetAll() 
        => await userServices.GetAllAsync<UserDto>();

    [HttpPost("user/post")]
    public async Task<IActionResult> Post([FromForm] UserCreateDto user)
    {
        await userServices.CreateAsync(user);
        return Ok();
    }

    [HttpPut("user-edit/{id}")]
    public async Task<IActionResult> Put([FromForm] UserEditDto userEditDto, int id)
    {
        await userServices.UpdateAsync(userEditDto, id);
        return Ok();
    }

    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userServices.DeleteAsync(u => u.Id == id);
        return Ok();
    }

}