using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.CreateAt)
            .HasDefaultValueSql("GETDATE()");
        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);
        
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}