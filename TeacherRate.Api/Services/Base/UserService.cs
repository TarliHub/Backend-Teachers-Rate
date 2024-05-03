using Microsoft.EntityFrameworkCore;
using TeacherRate.Domain.Interfaces.Base;
using TeacherRate.Domain.Models;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services.Base;

public class UserService : IUserService
{
    private readonly IRepository _repository;

    public UserService(IRepository repository)
    {
        _repository = repository;
    }

    public Task<List<TaskCategory>> GetCategories()
    {
        return _repository.GetAll<TaskCategory>().ToListAsync();
    }

    public Task<TaskCategory?> GetCategoryById(int id)
    {
        return _repository.GetById<TaskCategory>(id);
    }

    public Task<UserTask?> GetTaskById(int id)
    {
        return _repository.GetById<UserTask>(id);
    }

    public Task<List<UserTask>> GetTasks(int index, int size)
    {
        return _repository.GetAll<UserTask>(index, size).ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _repository.GetById<User>(id);
    }

    public async Task<User> UpdateUser(int id, User user)
    {
        throw new NotImplementedException();
    }
}
