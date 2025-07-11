using System.Linq.Expressions;
using TaskManager.Application.DTO;
using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Interfaces;

public interface IUserServices : IBaseService<User>
{
   
    public Task CreateAsync(UserCreateDto user);
    
    public Task UpdateAsync(User user);
    
    public Task DeleteAsync(int id);

}