using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.Data.Configuration;

public class UserAuthConfiguration : IEntityTypeConfiguration<UserAuth>
{

    public void Configure(EntityTypeBuilder<UserAuth> builder)
    {
        
        builder.ToTable("Auth");
        
        builder.HasKey(a => a.UserId);
        
        builder.Property(a => a.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Password)
            .IsRequired();
        
        builder.HasOne(a => a.User)
            .WithOne(u =>  u.UserAuth)
            .HasForeignKey<UserAuth>(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }

}