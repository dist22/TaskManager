namespace TaskManager.Domain.Interfaces;

public interface IPasswordHasher
{
    public string GeneratePasswordHash(string password);
    
    public bool Verify(string passwordHash, string password);
}