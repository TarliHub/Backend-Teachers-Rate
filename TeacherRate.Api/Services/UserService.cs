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
        var existedUser = await _repository.GetAll<User>().SingleOrDefaultAsync(t => t.Email == user.Email);

        if (existedUser != null)
            throw new ArgumentException($"User with email {existedUser.Email} already exists", nameof(existedUser));

        var entity = _repository.Add(user);
        var credentials = new CredentialsInfo() {
            User = entity,
            PasswordHash = PasswordManager.ComputeHashPassword(password)
        };
        _repository.Add(credentials);
        await _repository.SaveChanges();
        return entity;
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

    public IQueryable<HeadTeacher> GetHeadTeachers()
    {
        return _repository.GetAll<HeadTeacher>();
    }

    public IQueryable<Teacher> GetTeachers(int headTeacherId)
    {
        return _repository.GetAll<Teacher>().Where(t => t.HeadTeacher.Id == headTeacherId);
    }
}
