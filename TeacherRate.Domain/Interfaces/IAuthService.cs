using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface IAuthService
{
    Task<AuthData> Login(string email, string password);
    Task<User> GetMe(int id);
}
