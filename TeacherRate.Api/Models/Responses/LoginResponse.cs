using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Models.Responses;

public class LoginResponse
{
    public required string Token { get; set; }
    public required Role Role { get; set; }
}
