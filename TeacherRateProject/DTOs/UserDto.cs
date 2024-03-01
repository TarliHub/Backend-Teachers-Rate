using System.ComponentModel.DataAnnotations;

namespace TeacherRateProject.DTOs;

public class UserDto
{
    public required string Name { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Login { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Password { get; set; }
}
