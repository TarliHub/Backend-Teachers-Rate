using Microsoft.EntityFrameworkCore;
using TeacherRate.Domain.Models;

namespace TeacherRate.Storage;

public class TeacherRateContext : DbContext
{
    public TeacherRateContext(DbContextOptions<TeacherRateContext> options)
        : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    public DbSet<User> User { get; set; } = default!;
    public DbSet<TeacherBase> TeacherBase { get; set; } = default!;
    public DbSet<Teacher> Teacher { get; set; } = default!;
    public DbSet<HeadTeacher> HeadTeacher { get; set; } = default!;
    public DbSet<UserTask> UserTask { get; set; } = default!;
    public DbSet<TaskCategory> TaskCategory { get; set; } = default!;
    public DbSet<TeacherRequest> TeacherRequest { get; set; } = default!;
    public DbSet<CompletedTask> CompletedTask { get; set; } = default!;
    public DbSet<CredentialsInfo> CredentialsInfo { get; set; } = default!;
}
