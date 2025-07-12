namespace TaskManager.Domain.Models;

public class UserAuth
{
    public int  UserId { get; set; }
    
    public User? User { get; set; }
    
    public string Password { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
}