using TeacherRateProject.Data.Repository.Interfaces;
using TeacherRateProject.Data.Repository.SqlRepository;

namespace TeacherRateProject.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext context)
    {
        _context = context;
        User = new UserRepository(_context);
    }
    public IUserRepository User { get; private set; }

    public void Dispose()
    {
        _context.Dispose();
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}
