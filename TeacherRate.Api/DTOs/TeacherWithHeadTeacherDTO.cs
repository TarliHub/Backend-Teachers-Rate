namespace TeacherRate.Api.DTOs;

public class TeacherWithHeadTeacherDTO : TeacherBaseDTO
{
    public HeadTeacherDTO HeadTeacher { get; set; } = null!;
}
