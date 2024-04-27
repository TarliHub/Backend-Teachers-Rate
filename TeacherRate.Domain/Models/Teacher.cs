namespace TeacherRate.Domain.Models;

public class Teacher : TeacherBase
{
    public virtual HeadTeacher HeadTeacher { get; set; }
}
