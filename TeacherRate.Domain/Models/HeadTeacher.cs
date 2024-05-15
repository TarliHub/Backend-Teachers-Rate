namespace TeacherRate.Domain.Models;

public class HeadTeacher : TeacherBase
{
    public HeadTeacher()
    {
        Role = Role.HeadTeacher;
    }

    public required string CommissionName { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; } = null!;
}
