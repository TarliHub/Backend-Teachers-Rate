using System.Globalization;
using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface IUserService
{
    Task<List<Teacher>> GetTeachers(int headTeacherId, int index, int size);
    Task<List<HeadTeacher>> GetHeadTeachers(int index, int size);
    Task<T?> GetUserById<T>(int id) where T : User;
    Task<User> UpdateUser(User user);
    Task RemoveUser(int id);
    Task<User> AddUser(User user, string password);
}
