using Microsoft.EntityFrameworkCore;
using TeacherRate.Domain.Exceptions;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Helpers;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Api.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;

    public UserService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> AddUser(User user, string password)
    {
        var entity = _repository.Add(user);
        var credentials = new CredentialsInfo() {
            UserId = entity.Id,
            PasswordHash = PasswordManager.ComputeHashPassword(password)
        };
        await _repository.SaveChanges();
        return entity;
    }

    public async Task<List<Teacher>> GetTeachers(int headTeacherId, int index, int size)
    {
        var headTeacher = await _repository.GetById<HeadTeacher>(headTeacherId);
        if(headTeacher is null)
            throw new DetailedException("not found", nameof(headTeacher));

        return headTeacher.Teachers.ToList();
    }

    public Task<List<HeadTeacher>> GetHeadTeachers(int index, int size)
    {
        return _repository.GetAll<HeadTeacher>(index, size).ToListAsync();
    }

    public Task<T?> GetUserById<T>(int id) where T : User
    {
        return _repository.GetById<T>(id);
    }

    public async Task RemoveUser(int id)
    {
        _repository.Remove<User>(id);
        await _repository.SaveChanges();
    }

    public async Task<User> UpdateUser(User user)
    {
        var entity = _repository.Update(user);
        await _repository.SaveChanges();
        return entity;
    }
}
