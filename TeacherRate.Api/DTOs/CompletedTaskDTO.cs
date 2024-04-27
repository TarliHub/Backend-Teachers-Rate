using TeacherRate.Domain.Models;

namespace TeacherRate.Api.DTOs;

public class CompletedTaskDTO
{
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public UserTaskDTO Task { get; set; } = null!;
    public int Points { get; set; }
    public int Count { get; set; }
}
