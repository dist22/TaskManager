using System.Linq.Expressions;
using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Services;

public class UserServices(IBaseRepository<User> userRepository, IMapper mapper) : IUserServices
{
  
    public async Task<User> GetAsync(int id) 
        => await userRepository.GetAsync(u => u.Id == id) ?? 
           throw new Exception("User not found");
    
    public async Task<IEnumerable<User>> GetAllAsync() 
        => await userRepository.GetAllAsync();

    public async Task CreateAsync(UserCreateDto user)
    {
        var result = await userRepository.AddAsync(mapper.Map<User>(user));
        if(!result)
            throw new Exception("404");
    }

    public async Task UpdateAsync(User user)
    {
        var result = await userRepository.UpdateAsync(user);
        if(!result)
            throw new Exception("404");
    }

    public async Task DeleteAsync(int id)
    {
        var result = await userRepository.DeleteAsync(
            await userRepository.GetAsync(u => u.Id == id) ??
                throw new Exception("404"));
        if(!result)
            throw new Exception("404");
    }
}