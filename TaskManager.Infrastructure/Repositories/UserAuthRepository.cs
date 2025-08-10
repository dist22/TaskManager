using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Data.Context;

namespace TaskManager.Infrastructure.Repositories;

public class UserAuthRepository(DataContextEf entity) : BaseRepository<UserAuth>(entity), IUserAuthRepository
{
    public async Task<UserAuth?> GetUserAuthAsync(string email) 
        => await _dbSet
            .Include(ua => ua.User)
            .FirstOrDefaultAsync(ua => ua.Email == email);
}