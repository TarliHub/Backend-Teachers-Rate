using TeacherRateProject.DTOs;
using TeacherRateProject.Models;
using TeacherRateProject.Models.Paging;
using TeacherRateProject.Services.Interfaces;

namespace TeacherRateProject.Tests.Fakes;

internal class UserServiceFake : IUserService<User, int>
{
    private readonly List<User> _users;
    private static int _id = 1;

    public UserServiceFake()
    {
        _users = new List<User>();
    }
    public async Task<User> Add(User user)
    {
        user.Id = _id;
        _id++;
        _users.Add(user);
        return user;
    }

    public async Task Delete(int id)
    {
        _users.Remove(await GetById(id));
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return _users;
    }

    public async Task<User> GetById(int id)
    {
        return _users.First(u => u.Id == id);
    }

    public Task<PageList<User>> GetPaged(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> Query(Predicate<User> predicate)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, User user)
    {
        throw new NotImplementedException();
    }

    public Task<CompletedTaskDto> AddTaskToUser(int userId, PostCompletedTaskDto task)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CompletedTaskDto>> GetUserTasks(int userId)
    {
        throw new NotImplementedException();
    }
}
