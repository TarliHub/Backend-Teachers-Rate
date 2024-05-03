using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces.Base;

public interface IUserService
{
    Task<List<UserTask>> GetTasks(int index, int size);
    Task<List<TaskCategory>> GetCategories();
    Task<UserTask?> GetTaskById(int id);
    Task<TaskCategory?> GetCategoryById(int id);
    Task<User?> GetUserById(int id);
    Task<User> UpdateUser(int id, User user);
}
