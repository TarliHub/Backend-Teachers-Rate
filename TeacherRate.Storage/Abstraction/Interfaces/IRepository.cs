namespace TeacherRate.Storage.Abstraction.Interfaces;

public interface IRepository
{
    Task<T?> GetById<T>(int id) where T : class;
    Task<IQueryable<T>> GetAll<T>() where T : class;
    Task<IQueryable<T>> GetAll<T>(int index, int size) where T : class;
    Task<T> Add<T>(T user) where T : class;
    Task<bool> Remove<T>(int id) where T : class;
    Task<T> Update <T>(int id, T user) where T : class;
}
