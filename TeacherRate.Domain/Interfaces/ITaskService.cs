using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface ITaskService
{
    Task<bool> SendTask(TeacherRequest request, int teacherId);
    IQueryable<UserTask> GetTasks();
    Task<IQueryable<CompletedTask>> GetUserTasks<T>(int id) where T : TeacherBase;
    Task<UserTask?> GetTaskById(int id);
    Task<UserTask> AddTask(UserTask task, int categoryId);
    Task RemoveTask(int id);
    Task<UserTask> UpdateTask(UserTask task);
}
