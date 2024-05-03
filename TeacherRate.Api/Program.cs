using Microsoft.EntityFrameworkCore;
using TeacherRate.Storage;

namespace TeacherRate.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDependencies();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
        });

        builder.Services.AddDbContext<TeacherRateContext>(
            options => {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
                var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
                var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "teachers_rate_db";
                var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "user";
                var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password12345";
                options.UseNpgsql($"Host={dbHost}; Port={dbPort}; Database={dbName}; Username={dbUser}; Password={dbPassword};");
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
