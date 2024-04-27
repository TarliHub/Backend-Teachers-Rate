using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;

    public UserService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskCategory>> GetCategories()
    {
        return await _repository.GetAll<TaskCategory>();
    }

    public Task<UserTask?> GetTaskById(int id)
    {
        return _repository.GetById<UserTask>(id);
    }

    public async Task<IEnumerable<UserTask>> GetTasks(int index, int size)
    {
        return await _repository.GetAll<UserTask>(index, size);
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _repository.GetById<User>(id);
    }

    public async Task<User> UpdateUser(int id, User user)
    {
        return await _repository.Update(id, user);
    }
}
