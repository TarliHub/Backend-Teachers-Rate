using Microsoft.EntityFrameworkCore;
using TeacherRateProject.Data.Repository.Interfaces.Base;
using TeacherRateProject.Models.Paging;

namespace TeacherRateProject.Data.Repository.Implementation;

public class GenericRepository<T> : IGenericRepository<T, int> where T : class
{
    protected readonly DataContext _context;

    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task Delete(int id)
    {
        _context.Set<T>().Remove(await GetById(id));    
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<PageList<T>> GetPaged(int page, int pageSize)
    {
        int count = _context.Set<T>().Count();
        var items = await _context.Set<T>().Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new()
        {
            Items = new List<T>(items),
            PageSize = pageSize,
            PageIndex = page,
            PagesCount = (int)Math.Ceiling((double)count / pageSize),
        };
    }

    public async Task<T> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id)
            ?? throw new ArgumentNullException("entity", "could not found the entity");
    }

    public async Task<IEnumerable<T>> Query(Predicate<T> predicate)
    {
        return await _context.Set<T>().Where(entity => predicate(entity))
            .ToListAsync();
    }

    public async Task Update(int id, T entity)
    {
        var oldEntity = await GetById(id);
        oldEntity = entity;
        _context.Set<T>().Update(oldEntity);
    }
}
