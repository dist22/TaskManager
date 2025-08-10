using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Data.Context;

namespace TaskManager.Infrastructure.Repositories;

public class UserTaskRepository(DataContextEf entity) : BaseRepository<UserTask>(entity), IUserTaskRepository
{
    public async Task<IEnumerable<UserTask>> GetUsersByTaskIdAsync(int taskId)
    {
        return await _dbSet
            .Include(ut => ut.User)
            .Where(ut => ut.TaskId == taskId)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserTask>> GetTasksByUserIdAsync(int userId)
    {
        return await _dbSet
            .Include(ut => ut.Task)
            .Where(ut => ut.UserId == userId)
            .ToListAsync();
    }
}