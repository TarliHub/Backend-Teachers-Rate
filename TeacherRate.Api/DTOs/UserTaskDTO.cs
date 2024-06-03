namespace TeacherRate.Api.DTOs;

public class UserTaskDTO
{
    public UserTaskDTO()
    {
        Points = new();
    }

    public int Id { get; set; }
    public required string Title { get; set; }
    public string? PointsDescription { get; set; }
    public List<int> Points { get; set; }
    public required string Approval { get; set; }
    public TaskCategoryDTO Category { get; set; } = null!;
}
