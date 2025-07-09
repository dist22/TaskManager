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
        => Ok(await userServices.GetAsync(id));

    [HttpGet("all")]
    public async Task<IEnumerable<User>> GetAll() 
        => await userServices.GetAllAsync();

    [HttpPost("user/post")]
    public async Task<IActionResult> Post([FromForm] UserCreateDto user)
    {
        await userServices.CreateAsync(user);
        return Ok();
    }

    [HttpPut("user-edit/{id}")]
    public async Task<IActionResult> Put([FromForm] User user, int id)
    {
        await userServices.UpdateAsync(user);
        return Ok();
    }

    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userServices.DeleteAsync(id);
        return Ok();
    }

}