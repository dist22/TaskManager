using TaskManager.Domain.Models;
using TaskManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        await userServices.CreateAsync(user);
        return Ok();
    }

    [HttpPut("user-edit/{id}")]
    public async Task<IActionResult> Put([FromBody] User user, int id)
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