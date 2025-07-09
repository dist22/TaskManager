using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.Data.Configuration;

public class TaskTimeConfigure : IEntityTypeConfiguration<TaskTime>
{
    public void Configure(EntityTypeBuilder<TaskTime> builder)
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