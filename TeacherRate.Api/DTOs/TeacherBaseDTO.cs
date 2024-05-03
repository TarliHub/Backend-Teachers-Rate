using TeacherRate.Domain.Models;

namespace TeacherRate.Api.DTOs;

public class TeacherBaseDTO
{
    public List<CompletedTaskDTO> Tasks { get; set; } = null!;
    public int Points { get; set; }
}
