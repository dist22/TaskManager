namespace TaskManager.Domain.Models;

public class UserAuth
{
    public int  UserId { get; set; }
    
    public User? User { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
}