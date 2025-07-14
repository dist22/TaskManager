using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Domain.Models;

public class TaskTime : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreateAt { get; set; } 
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public int CategoryId { get; set; }
    public IEnumerable<UserTask> UserTasks { get; set; } = new List<UserTask>();
    public Category? Category { get; set; }
    
}