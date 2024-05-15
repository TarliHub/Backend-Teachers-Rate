using TeacherRate.Domain.Models;

namespace TeacherRate.Api.DTOs;

public class TeacherRequestDTO
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public required string ApprovalLink { get; set; }
    public string? Description { get; set; }
    public int Points { get; set; }
}
