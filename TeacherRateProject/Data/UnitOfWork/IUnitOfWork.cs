using TeacherRateProject.Data.Repository.Interfaces;

namespace TeacherRateProject.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository User { get; }
    IRatingTaskRepository RatingTask { get; }
    ICompletedTaskRepository CompletedTask { get; }

    int Save();
}
