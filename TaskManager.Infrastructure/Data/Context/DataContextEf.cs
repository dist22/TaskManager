using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Data.Configuration;

namespace TaskManager.Infrastructure.Data.Context;

public class DataContextEf(DbContextOptions<DataContextEf> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskTime> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TaskTimeConfigure());
        modelBuilder.ApplyConfiguration(new CategorieConfiguration());
        modelBuilder.ApplyConfiguration(new UserTasksConfiguration());
        modelBuilder.ApplyConfiguration(new UserAuthConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}