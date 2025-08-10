using System.Threading.Tasks;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces.Repositories;

public interface IUserAuthRepository : IBaseRepository<UserAuth>
{
    public Task<UserAuth?> GetUserAuthAsync(string email);
}