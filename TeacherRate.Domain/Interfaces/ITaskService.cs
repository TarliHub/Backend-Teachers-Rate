using TeacherRate.Domain.Models;

namespace TeacherRate.Domain.Interfaces;

public interface ITaskService
{
    Task<bool> SendTask(TeacherRequest request);
    Task<List<CompletedTask>> GetCompletedTasks(int index, int size);
    IQueryable<UserTask> GetTasks();
    Task<List<CompletedTask>> GetUserTasks<T>(int id) where T : TeacherBase;
    Task<UserTask?> GetTaskById(int id);
    Task<UserTask> AddTask(UserTask task);
    Task RemoveTask(int id);
    Task<UserTask> UpdateTask(UserTask task);
}
