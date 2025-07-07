using System.Linq.Expressions;
using TaskManager.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Interfaces;

public interface IUserServices
{

    public Task<User> GetAsync(int id);
    
    public Task<IEnumerable<User>> GetAllAsync();
    
    public Task CreateAsync(User user);
    
    public Task UpdateAsync(User user);
    
    public Task DeleteAsync(int id);

}