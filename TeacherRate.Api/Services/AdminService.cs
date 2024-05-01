using TeacherRate.Api.Services.Base;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services;

public class AdminService : UserService, IAdminService
{
    private readonly IRepository _repository;

    public AdminService(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public Task<TaskCategory> AddCategory(TaskCategory category)
    {
        return _repository.Add(category);
    }

    public Task<HeadTeacher> AddHeadTeacher(HeadTeacher headTeacher)
    {
        return _repository.Add(headTeacher);
    }

    public Task<UserTask> AddTask(UserTask task)
    {
        return _repository.Add(task);
    }

    public Task<HeadTeacher?> GetHeadTeacherById(int id)
    {
        return _repository.GetById<HeadTeacher>(id);
    }

    public async Task<IEnumerable<HeadTeacher>> GetHeadTeachers()
    {
        return await _repository.GetAll<HeadTeacher>();
    }

    public Task<bool> RemoveCategory(int id)
    {
        return _repository.Remove<TaskCategory>(id);
    }

    public Task<bool> RemoveHeadTeacher(int id)
    {
        return _repository.Remove<HeadTeacher>(id);
    }

    public Task<bool> RemoveTask(int id)
    {
        return _repository.Remove<UserTask>(id);
    }

    public Task<TaskCategory> UpdateCategory(int id, TaskCategory category)
    {
        return _repository.Update(id, category);
    }

    public Task<HeadTeacher> UpdateHeadTeacher(int id, HeadTeacher headTeacher)
    {
        return _repository.Update(id, headTeacher);
    }

    public Task<UserTask> UpdateTask(int id, UserTask task)
    {
        return _repository.Update(id, task);
    }
}
