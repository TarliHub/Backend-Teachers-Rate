using System.ComponentModel.DataAnnotations;

namespace TeacherRateProject.Models;

public class TaskApprove
{
    public int Id { get; set; }

    [Required]
    [StringLength(256)]
    public required string Title { get; set; }
    public virtual ICollection<RatingTask> Tasks { get; set; } = null!;
}
