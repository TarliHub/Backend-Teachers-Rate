using TeacherRate.Api.DTOs;

namespace TeacherRate.Api.Models.Requests;

public class CreateTaskRequest
{
    public required string Title { get; set; }
    public string? PointsDescription { get; set; }
    public List<int> Points { get; set; }
    public required string Approval { get; set; }
    public int CategoryId { get; set; }
}
