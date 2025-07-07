using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;
using Task = TaskManager.Domain.Models.Task;

namespace TaskManager.Infrastructure.Data.Configuration;

public class TaskTimeConfigure : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Tasks");
        
        builder.HasKey(f => f.Id);

        builder.Property(f => f.CreateAt)
            .HasDefaultValueSql("getdate()");
        builder.Property(f => f.Priority)
            .HasConversion<string>()
            .HasDefaultValue(TaskPriority.Medium);
        
        builder.Property(f => f.CategoryId)
            .IsRequired();

        builder.HasIndex(f => f.CategoryId);
        builder.HasIndex(f => f.Priority);
        builder.HasIndex(f => f.DueDate);
        
        builder.HasOne(f => f.Category)
            .WithMany(f => f.Tasks)
            .HasForeignKey(f => f.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}