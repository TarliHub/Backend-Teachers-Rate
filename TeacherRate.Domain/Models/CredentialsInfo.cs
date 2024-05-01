using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherRate.Domain.Models;

public class CredentialsInfo
{
    public int Id { get; set; }
    public required string PasswordHash { get; set; }

    [ForeignKey("FK_Creds_User")]
    public int UserId { get; set; }
}
