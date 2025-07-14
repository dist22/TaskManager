using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.Data.Configuration;

public class UserTasksConfiguration : IEntityTypeConfiguration<UserTask>
{
    public void Configure(EntityTypeBuilder<UserTask> builder)
    {
        
        builder.ToTable("UserTasks");
        
        builder.HasKey(uf => new { uf.UserId, uf.TaskId });
        
        builder.Property(uf => uf.AssignedAt)
            .HasDefaultValueSql("getdate()");

        builder.Property(uf => uf.IsCompeted)
            .HasDefaultValue(false);

        builder.HasIndex(ut => ut.IsCompeted);
        builder.HasIndex(uf => uf.AssignedAt);
        
        builder.HasOne(ut => ut.User)
            .WithMany(u => u.UserTasks)
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(ut => ut.Task)
            .WithMany(t => t.UserTasks)
            .HasForeignKey(ut => ut.TaskId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}