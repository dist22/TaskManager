using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Application.DTO;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces;

public interface IUserAuthService
{
    public Task RegisterUserAsync(UserCreateDto userCreateDto);
    
    public Task<Dictionary<string, string>> LoginUserAsync(UserLoginDto userLoginDto);
}