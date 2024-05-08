namespace TeacherRate.Domain.Models;

public class AuthData
{
    public required string Token { get; set; }
    public Role Role { get; set; }
    public int UserId { get; set; }
}
