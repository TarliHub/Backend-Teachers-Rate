using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherRate.Domain.Models;

public class TeacherBase : User
{
    public TeacherBase()
    {
        Tasks = new List<CompletedTask>();
    }

    public virtual ICollection<CompletedTask> Tasks { get; set; }

    [NotMapped]
    public int Points => Tasks.Sum(t => t.Points);
}
