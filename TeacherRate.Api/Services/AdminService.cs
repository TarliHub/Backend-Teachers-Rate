using Microsoft.EntityFrameworkCore;
using TeacherRate.Api.Services.Base;
using TeacherRate.Domain.Exceptions;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Helpers;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services;

public class AdminService : UserService, IAdminService
{
    private readonly IRepository _repository;

    public AdminService(IRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public async Task<TaskCategory> AddCategory(TaskCategory category)
    {
        var entity = _repository.Add(category);
        await _repository.SaveChanges();
        return entity;
    }

    public async Task<HeadTeacher> AddHeadTeacher(HeadTeacher headTeacher, string password)
    {
        var entity = _repository.Add(headTeacher);
        var creds = new CredentialsInfo()
        {
            PasswordHash = PasswordManager.ComputeHashPassword(password),
            UserId = entity.Id,
        };
        _repository.Add(creds);
        await _repository.SaveChanges();
        return entity;
    }

    public async Task<UserTask> AddTask(UserTask task)
    {
        var entity = _repository.Add(task);
        await _repository.SaveChanges();
        return entity;
    }

    public Task<HeadTeacher?> GetHeadTeacherById(int id)
    {
        return _repository.GetById<HeadTeacher>(id);
    }

    public Task<List<HeadTeacher>> GetHeadTeachers(int index, int size)
    {
        return _repository.GetAll<HeadTeacher>(index, size).ToListAsync();
    }

    public async Task<List<CompletedTask>> GetHeadTeacherTasks(int id, int index, int size)
    {
        var headTeacher = await _repository.GetById<HeadTeacher>(id);

        if (headTeacher is null)
            throw new DetailedException("not found", nameof(headTeacher));

        return headTeacher.Tasks.Skip(index).Take(size).ToList();
    }

    public Task<bool> RemoveCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveHeadTeacher(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveTask(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TaskCategory> UpdateCategory(int id, TaskCategory category)
    {
        throw new NotImplementedException();
    }

    public Task<HeadTeacher> UpdateHeadTeacher(int id, HeadTeacher headTeacher)
    {
        throw new NotImplementedException();
    }

    public Task<UserTask> UpdateTask(int id, UserTask task)
    {
        throw new NotImplementedException();
    }
}
