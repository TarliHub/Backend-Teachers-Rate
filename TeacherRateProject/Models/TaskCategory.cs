using System.ComponentModel.DataAnnotations;

namespace TeacherRateProject.Models;

public class TaskCategory
{
    public int Id { get; set; }

    [Required]
    [StringLength(256)]
    public string Title { get; set; }
    public virtual ICollection<RatingTask> Tasks { get; set; } = null!;
}
