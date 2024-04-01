using Microsoft.EntityFrameworkCore;
using TeacherRateProject.Models;

namespace TeacherRateProject.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ProxiesExtensions.UseLazyLoadingProxies(optionsBuilder);
    }

    public DbSet<User> User { get; set; } = default!;
}
