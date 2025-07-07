using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data.Context;

namespace TaskManager.Infrastructure.Repositories;

public class BaseRepository<T>(DataContextEf entity) : IBaseRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = entity.Set<T>();
    
    private async Task<bool> SaveChangesAsync()
        => await entity.SaveChangesAsync() > 0;

    public async Task<bool> UpdateAsync(T @object)
    {
        _dbSet.Update(@object);
        return await SaveChangesAsync();
    }

    public async Task<bool> AddAsync(T @object)
    {
        await _dbSet.AddAsync(@object);
        return await SaveChangesAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T?, bool>> expression) 
        => await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(expression);

    public async Task<IEnumerable<T>> GetAllAsync() 
        => await  _dbSet
            .AsNoTracking()
            .ToListAsync();

    public async Task<bool> DeleteAsync(T @object)
    {
        entity.Remove(@object);
        return await SaveChangesAsync();
    }

    public async Task<bool> IfExistAsync(Expression<Func<T, bool>> expression)
        => await _dbSet
            .AsNoTracking()
            .AnyAsync(expression);
}