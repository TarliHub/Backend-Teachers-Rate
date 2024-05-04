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

    public Task<UserTask> AddTask(UserTask task)
    {
        throw new NotImplementedException();
    }

    public Task<List<CompletedTask>> GetCompletedTasks(int index, int size)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask?> GetTaskById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserTask>> GetTasks(int index, int size)
    {
        throw new NotImplementedException();
    }

    public Task<List<CompletedTask>> GetUserTasks<T>(int id) where T : TeacherBase
    {
        throw new NotImplementedException();
    }

    public Task RemoveTask(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SendTask(TeacherRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> UpdateTask(UserTask task)
    {
        throw new NotImplementedException();
    }
}
