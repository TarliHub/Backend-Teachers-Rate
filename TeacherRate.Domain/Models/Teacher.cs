using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherRate.Domain.Models;

public class Teacher : TeacherBase
{
    public Teacher()
    {
        Role = Role.Teacher;
    }

    public virtual HeadTeacher HeadTeacher { get; set; } = null!;
}
