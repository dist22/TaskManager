using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Domain.Models;
using TaskManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTO;
using TaskManager.Domain.Enums;
using TaskManager.WebAPI.Extensions;

namespace TaskManager.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController(IUserServices userServices) : ControllerBase
{
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetAsyncController(int id)
        => Ok(await userServices.GetAsync<UserDto>(id));

    [HttpGet("all")]
    public async Task<IEnumerable<UserDto>> GetAllAsyncController() 
        => await userServices.GetAllAsync<UserDto>();

    [HttpGet("log-user")]
    public async Task<UserDto> GetloginedUserAsyncController()
        => await userServices.GetAsync<UserDto>(this.GetUserId());

    [HttpGet("filter-sort")]
    public IEnumerable<UserDto> FilterAsync([FromQuery]UserFilterSortDto userFilterSortDto) 
        => userServices.FilterSortUser(userFilterSortDto);
    

    [HttpPut("user/{id}/edit")]
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