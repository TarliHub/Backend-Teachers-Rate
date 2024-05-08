using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherRate.Domain.Models;

public class CredentialsInfo
{
    public int Id { get; set; }
    public required string PasswordHash { get; set; }
    public virtual User User { get; set; } = null!;
}
