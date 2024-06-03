using System.Globalization;
using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface IUserService
{
    IQueryable<Teacher> GetTeachers(int headTeacherId);
    IQueryable<HeadTeacher> GetHeadTeachers();
    IQueryable<CompletedTask> GetCompletedTasks(int userId);
    Task<T?> GetUserById<T>(int id) where T : User;
    Task<User> UpdateUser(User user, string password);
    Task RemoveUser(int id);
    Task<User> AddUser(User user, string password);
}
