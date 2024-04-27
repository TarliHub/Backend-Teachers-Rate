using TeacherRate.Domain.Models;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Storage.Repository;

public class FakeRepository : IRepository
{

    public Task<T> Add<T>(T user) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<T>> GetAll<T>() where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<T>> GetAll<T>(int index, int size) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetById<T>(int id) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<bool> Remove<T>(int id) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> Update<T>(int id, T user) where T : class
    {
        throw new NotImplementedException();
    }
}
