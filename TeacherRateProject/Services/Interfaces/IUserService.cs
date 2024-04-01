using TeacherRateProject.DTOs;

namespace TeacherRateProject.Services.Interfaces;

public interface IUserService<T, TKey> 
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(TKey id);
    Task<IEnumerable<T>> GetPaged(int page, int pageSize);
    Task<IEnumerable<T>> Query(Predicate<T> predicate);
    Task<T> Add(T user);
    Task Delete(TKey id);
    Task Update(TKey id, T user);
    Task<CompletedTaskDto> AddTaskToUser(int userId, PostCompletedTaskDto task);
    Task<IEnumerable<CompletedTaskDto>> GetUserTasks(int userId);
}
