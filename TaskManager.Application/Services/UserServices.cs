using System.Linq.Expressions;
using AutoMapper;
using TaskManager.Application.DTO;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Services;

public class UserServices(IBaseRepository<User> userRepository, IMapper mapper) : BaseService, IUserServices
{
    public async Task<User> GetAsync(int id)
        => await GetIfNotNull(userRepository.GetAsync(u => u.Id == id));

    public async Task<IEnumerable<User>> GetAllAsync()
        => await userRepository.GetAllAsync();

    public async Task CreateAsync(UserCreateDto user)
    {
        await EnsureSuccess(userRepository.AddAsync(mapper.Map<User>(user)));
    }

    public async Task UpdateAsync(User user)
    {
        await EnsureSuccess(userRepository.UpdateAsync(user));
    }

    public async Task DeleteAsync(int id)
    {
        await EnsureSuccess(
            userRepository.DeleteAsync(
                await GetIfNotNull(
                    userRepository.GetAsync(u => u.Id == id))));
    }
}