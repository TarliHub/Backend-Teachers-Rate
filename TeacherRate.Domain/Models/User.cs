using System.ComponentModel.DataAnnotations;

namespace TeacherRate.Domain.Models;

public class User
{
    public int Id { get; set; }

    [StringLength(64)]
    public required string Name { get; set; }

    [StringLength(64)]
    public required string LastName { get; set; }

    [StringLength(64)]
    [EmailAddress]
    public required string Email { get; set; }

    public DateTime CreatedAt { get; set; }
}
