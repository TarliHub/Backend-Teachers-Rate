using System.ComponentModel.DataAnnotations;

namespace TeacherRateProject.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Login { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Password { get; set; }
}
