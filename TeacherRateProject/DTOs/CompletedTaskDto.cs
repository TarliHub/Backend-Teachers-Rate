namespace TeacherRateProject.DTOs;

public class CompletedTaskDto
{
    public int Id { get; set; }
    public RatingTaskDto Task { get; set; } = null!;
    public int UserId { get; set; }
    public int ActualRating { get; set; }
}
