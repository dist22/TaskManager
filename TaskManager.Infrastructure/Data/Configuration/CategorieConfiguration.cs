using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.Data.Configuration;

public class CategorieConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.CreateAt)
            .HasDefaultValueSql("getdate()");
        
        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasIndex(c => c.Name)
            .IsUnique();
        
        builder.HasMany(c => c.Tasks)
            .WithOne(f => f.Category)
            .HasForeignKey(f => f.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}