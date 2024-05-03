using TeacherRate.Domain.Models;

namespace TeacherRate.Api.DTOs;

public class TeacherDTO : TeacherBaseDTO
{
    public HeadTeacherDTO HeadTeacher { get; set; } = null!;
}
