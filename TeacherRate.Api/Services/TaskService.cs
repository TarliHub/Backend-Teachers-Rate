using Microsoft.EntityFrameworkCore;
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

    public async Task<UserTask> AddTask(UserTask task)
    {
        if(task.Category ==  null) 
            throw new ArgumentNullException("Category must not be null", nameof(task.Category));

        if(string.IsNullOrEmpty(task.Title))
            throw new ArgumentNullException("Title must not be empty", nameof(task.Title));

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

    public async Task<bool> SendTask(TeacherRequest request)
    {
        try
        {
            _repository.Add(request);
            var completedTask = new CompletedTask()
            {
                Task = request.Task,
                Points = request.Points,
                Count = 1,
                Teacher = request.Teacher,
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
