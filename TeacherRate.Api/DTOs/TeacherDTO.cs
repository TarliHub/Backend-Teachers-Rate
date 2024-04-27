using TeacherRate.Domain.Models;

namespace TeacherRate.Api.DTOs;

public class TeacherDTO : UserDTO
{
    public List<CompletedTask> Tasks { get; set; } = null!;
    public int Points { get; set; }
}
