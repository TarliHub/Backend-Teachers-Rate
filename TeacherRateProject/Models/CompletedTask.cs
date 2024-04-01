namespace TeacherRateProject.Models;

public class CompletedTask
{
    public int Id { get; set; }
    public virtual RatingTask Task { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public int ActualRating { get; set; }
}
