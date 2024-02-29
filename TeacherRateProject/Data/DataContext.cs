using Microsoft.EntityFrameworkCore;
using TeacherRateProject.Models;

namespace TeacherRateProject.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<User> User { get; set; } = default!;
}
