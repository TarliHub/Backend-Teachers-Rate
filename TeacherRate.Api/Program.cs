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
        builder.Services.AddDbContext<TeacherRateContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
