using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Domain.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; }
    public bool IsActive { get; set; }
    public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
    public UserAuth? UserAuth { get; set; }
    
}