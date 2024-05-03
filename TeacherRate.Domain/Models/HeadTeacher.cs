namespace TeacherRate.Domain.Models;

public class HeadTeacher : TeacherBase
{
    public HeadTeacher()
    {
        Role = Role.HeadTeacher;
    }

    public virtual ICollection<Teacher> Teacher { get; set; } = null!;
}
