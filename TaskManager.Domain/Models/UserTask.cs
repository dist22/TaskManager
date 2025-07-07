namespace TaskManager.Domain.Models;

public class UserTask
{
    
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public DateTime AssignedAt { get; set; }
    public bool IsCompete { get; set; }
    public DateTime? CompetedAt { get; set; }
    
    public User? User { get; set; }
    public Task? Task { get; set; }

}