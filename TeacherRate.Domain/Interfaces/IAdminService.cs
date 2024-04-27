using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface IAdminService : IUserService
{
    Task<UserTask> AddTask(UserTask task);
    Task<bool> RemoveTask(int id);
    Task<UserTask> UpdateTask(int id, UserTask task);
}
