using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Linq.Expressions;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Storage.Repository;

public class GenericRepository : IRepository
{
    private readonly TeacherRateContext _context;

    public GenericRepository(TeacherRateContext context)
    {
        _context = context;
    }

    public async Task<T> Add<T>(T user) where T : class
    {
        var entity = await _context.Set<T>().AddAsync(user);
        return entity.Entity;
    }

    public async Task<IQueryable<T>> GetAll<T>() where T : class
    {
        return _context.Set<T>().AsQueryable();
    }

    public async Task<IQueryable<T>> GetAll<T>(int index, int size) where T : class
    {
        return _context.Set<T>().Skip(index).Take(size).AsQueryable();
    }

    public async Task<T?> GetById<T>(int id) where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public Task<T?> QueryOne<T>(Expression<Func<T, bool>> expression) where T : class
    {
        return _context.Set<T>().SingleOrDefaultAsync(expression);
    }

    public async Task<bool> Remove<T>(int id) where T : class
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity is null) return false;

        _context.Set<T>().Remove(entity);
        return true;
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }

    // Not sure if it works correctly
    public async Task<T> Update<T>(int id, T user) where T : class
    {
        var entity = _context.Set<T>().Update(user);
        return entity.Entity;
    }
}
