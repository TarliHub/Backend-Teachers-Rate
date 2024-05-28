using System.Text.Json.Serialization;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.DTOs;

public class TeacherBaseDTO : UserDTO
{
    public List<CompletedTaskDTO> Tasks { get; set; } = null!;
    public int Points { get; set; }
}
