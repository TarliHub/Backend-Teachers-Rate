namespace TeacherRate.Api.DTOs;

public class HeadTeacherWithTeachersDTO : TeacherBaseDTO
{
    public List<TeacherDTO> Teachers { get; set; } = null!;
}
