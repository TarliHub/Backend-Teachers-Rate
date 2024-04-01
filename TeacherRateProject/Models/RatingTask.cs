using System.ComponentModel.DataAnnotations;

namespace TeacherRateProject.Models;

public class RatingTask
{
    public int Id { get; set; }

    [Required]
    [StringLength(512)]
    public required string Title { get; set; }
    public int RatingPoints { get; set; }

    [StringLength(256)]
    public string? RatingTip { get; set; }
    public virtual TaskApprove Approve { get; set; } = null!;
    public virtual TaskCategory Category { get; set; } = null!;
}
