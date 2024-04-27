namespace TeacherRate.Domain.Models;

public class TeacherRequest
{
    public int Id { get; set; }
    public virtual TeacherBase Teacher { get; set; } = null!;
    public virtual UserTask Task { get; set; } = null!;
    public required string ApprovalLink { get; set; }
    public string? Description { get; set; }
    public User Reviewer { get; set; } = null!;
}
