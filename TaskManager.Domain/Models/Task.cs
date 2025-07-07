using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Models;

public class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } 
    public DateTime DueDate { get; set; }
    public bool IsComplete { get; set; } = false;
    public TaskPriority Priority { get; set; }

    public int CategoryId { get; set; }
    
    public IEnumerable<UserTask> UserTasks { get; set; } = new List<UserTask>();
    public Category? Category { get; set; }
}