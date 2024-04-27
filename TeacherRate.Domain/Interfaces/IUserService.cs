using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserTask>> GetTasks(int index, int size);
    Task<IEnumerable<TaskCategory>> GetCategories();
    Task<UserTask?> GetTaskById(int id);
    Task<User?> GetUserById(int id);
    Task<User> UpdateUser(int id, User user);
}
