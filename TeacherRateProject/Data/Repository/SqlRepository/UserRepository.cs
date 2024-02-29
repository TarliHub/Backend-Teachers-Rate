using Microsoft.EntityFrameworkCore;
using TeacherRateProject.Data.Repository.Interfaces;
using TeacherRateProject.Models;

namespace TeacherRateProject.Data.Repository.SqlRepository;

public class UserRepository : IUserRepository<User, int>
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<User> AddUser(User user)
    {
        _context.User.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return (await _context.User.FirstOrDefaultAsync(user => user.Id == id))
            ?? throw new ArgumentNullException(nameof(id), $"No user found with id = {id}");
    }
}
