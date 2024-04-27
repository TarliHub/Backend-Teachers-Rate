using System.ComponentModel.DataAnnotations;

namespace TeacherRate.Domain.Models;

public class TaskCategory
{
    public int Id { get; set; }

    [StringLength(128)]
    public required string Name { get; set; }
}
