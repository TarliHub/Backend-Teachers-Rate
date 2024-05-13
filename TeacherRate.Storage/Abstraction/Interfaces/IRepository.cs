using System.Linq.Expressions;

namespace TeacherRate.Storage.Abstraction.Interfaces;

public interface IRepository
{
    Task<T?> GetById<T>(int id) where T : class;
    IQueryable<T> GetAll<T>() where T : class;
    IQueryable<T> GetAll<T>(int index, int size) where T : class;
    T Add<T>(T user) where T : class;
    void Remove<T>(int id) where T : class;
    T Update<T>(T item) where T : class;
    Task<T?> QueryOne<T>(Expression<Func<T, bool>> expression) where T : class; 
    Task<int> SaveChanges();
}
