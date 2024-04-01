using System.ComponentModel.DataAnnotations;
using TeacherRateProject.Models;

namespace TeacherRateProject.DTOs;

public class UserDto
{
    public required string Name { get; set; }

    [Required]
    [MaxLength(64)]
    public required string LastName { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Login { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Password { get; set; }
    public DateTime RegisteredAt { get; set; }

    public Role Role { get; set; }

    public int Rating { get; set; }
}
