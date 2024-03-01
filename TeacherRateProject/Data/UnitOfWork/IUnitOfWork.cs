using TeacherRateProject.Data.Repository.Interfaces;

namespace TeacherRateProject.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }

    int Save();
}
