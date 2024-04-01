namespace TeacherRateProject.DTOs;

public class RatingTaskDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int RatingPoints { get; set; }
    public string? RatingTip { get; set; }
    public TaskApproveDto Approve { get; set; } = null!;
    public TaskCategoryDto Category { get; set; } = null!;
}
