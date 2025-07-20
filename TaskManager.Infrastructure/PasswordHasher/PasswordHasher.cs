using TaskManager.Application.Interfaces.PasswordHasher;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public string GeneratePasswordHash(string password) 
        => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string passwordHash) 
        => BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}