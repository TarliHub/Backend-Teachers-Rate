namespace TeacherRateProject.Data.Repository.Interfaces;

public interface IUserRepository<T, TKey> where T : class
{
    Task<IEnumerable<T>> GetAllUsers();
    Task<T> GetUserById(TKey id);
    Task<T> AddUser(T user);
}
