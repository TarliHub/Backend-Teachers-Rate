using TeacherRate.Domain.Models;

namespace TeacherRate.Api.DTOs;

public class TeacherDTO : UserDTO
{
    public HeadTeacherDTO HeadTeacher { get; set; } = null!;
}
