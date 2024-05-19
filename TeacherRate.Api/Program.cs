using TeacherRate.Api.AppConfiguration;

namespace TeacherRate.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("APP_SETTINGS") ?? "Development"}.json");

        builder.Services.ConfigureServices(builder);

        var app = builder.Build();

        app.ConfigureMiddlewares(app.Environment);

        app.MapControllers();

        app.Run();
    }
}
