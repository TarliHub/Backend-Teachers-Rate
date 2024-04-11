using TeacherRateProject.Models.Paging;

namespace TeacherRateProject.Data.Repository.Interfaces.Base;

public interface IGenericRepository<T, TKey> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(TKey id);
    Task<PageList<T>> GetPaged(int page, int pageSize);
    Task<IEnumerable<T>> Query(Predicate<T> predicate);
    Task<T> Add(T entity);
    Task Delete(TKey id);
    Task Update(TKey id, T entity);
}
