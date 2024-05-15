namespace TeacherRate.Api.DTOs;

public class HeadTeacherWithTeachersDTO : TeacherBaseDTO
{
    public required string CommissionName { get; set; }

    public List<TeacherDTO> Teachers { get; set; } = null!;
}
