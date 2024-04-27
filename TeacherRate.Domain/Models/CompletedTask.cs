namespace TeacherRate.Domain.Models;

public class CompletedTask
{
    public int Id { get; set; }
    public virtual TeacherBase Teacher { get; set; } = null!;
    public virtual UserTask Task { get; set; } = null!;
    public int Points { get; set; }
    public int Count { get; set; }
}
