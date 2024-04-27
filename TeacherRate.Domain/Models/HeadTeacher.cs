namespace TeacherRate.Domain.Models;

public class HeadTeacher : TeacherBase
{
    public virtual ICollection<Teacher> Teacher { get; set; }
}
