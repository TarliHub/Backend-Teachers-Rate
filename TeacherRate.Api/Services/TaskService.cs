using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services;

public class TaskService : ITaskService
{
    private readonly IRepository _repository;

    public TaskService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserTask> AddTask(UserTask task, int categoryId)
    {
        var category = await _repository.GetById<TaskCategory>(categoryId);

        if(category == null) 
            throw new ArgumentNullException("Category must not be null", nameof(category));

        if(string.IsNullOrEmpty(task.Title))
            throw new ArgumentNullException("Title must not be empty", nameof(task.Title));

        task.Category = category;
        var entity = _repository.Add(task);
        await _repository.SaveChanges();
        return entity;
    }

    public Task<UserTask?> GetTaskById(int id)
    {
        return _repository.GetById<UserTask>(id);
    }

    public IQueryable<UserTask> GetTasks()
    {
        return _repository.GetAll<UserTask>();
    }

    public async Task<IQueryable<CompletedTask>> GetUserTasks<T>(int id) where T : TeacherBase
    {
        var user = await _repository.GetById<T>(id);
        if (user == null)
            throw new ArgumentException($"User not found with id {id}", nameof(id));

        return user.Tasks.AsQueryable();
    }

    public Task RemoveTask(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SendTask(TeacherRequest request, int teacherId)
    {
        try
        {
            var task = await _repository.GetById<UserTask>(request.Task.Id);
            var teacher = await _repository.GetById<TeacherBase>(teacherId);
            request.Task = task;
            request.Teacher = teacher;
            _repository.Add(request);

            var completedTask = new CompletedTask()
            {
                Task = task,
                Points = request.Points,
                Count = 1,
                Teacher = teacher,
            };

            _repository.Add(completedTask);
            await _repository.SaveChanges();
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }

    public Task<UserTask> UpdateTask(UserTask task)
    {
        throw new NotImplementedException();
    }
}
