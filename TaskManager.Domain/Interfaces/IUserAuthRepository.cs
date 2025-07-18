using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces;

public interface IUserAuthRepository : IBaseRepository<UserAuth>
{
    public Task<UserAuth?> GetUserAuthAsync(string email);
}