using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public string GeneratePasswordHash(string password) 
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string passwordHash, string password) 
        => BCrypt.Net.BCrypt.Verify(passwordHash, password);
}