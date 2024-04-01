using System.ComponentModel.DataAnnotations;

namespace TeacherRateProject.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(64)]
    public required string LastName { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Login { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Password { get; set; }

    public DateTime RegisteredAt { get; set; }

    public Role Role { get; set; }

    public int Rating => Tasks.Select(t => t.ActualRating).Sum();

    public virtual ICollection<CompletedTask> Tasks { get; set; } = null!;
}
