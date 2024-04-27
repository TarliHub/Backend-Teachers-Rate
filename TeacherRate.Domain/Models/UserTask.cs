using System.ComponentModel.DataAnnotations;

namespace TeacherRate.Domain.Models;

public class UserTask
{
    public int Id { get; set; }

    [StringLength(256)]
    public required string Title { get; set; }
    public string? PointsDescription { get; set; }
    public required string Approval { get; set; }
    public virtual TaskCategory Category { get; set; } = null!;
}
