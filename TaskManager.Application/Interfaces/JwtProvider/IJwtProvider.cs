using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces.JwtProvider;

public interface IJwtProvider
{
    public string CreateToken(User user);
}