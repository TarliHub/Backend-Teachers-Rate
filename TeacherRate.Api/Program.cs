using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TeacherRate.Storage;

namespace TeacherRate.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("APP_SETTINGS") ?? "Development"}.json");
        builder.Services.AddControllers();
        builder.Services.AddDependencies();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration.GetSection("AppSettings:Token").Value!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        builder.Services.AddDistributedMemoryCache();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(
                int.Parse(builder.Configuration.GetSection("AppSettings:ExpireTime").Value!));
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

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
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseSession();

        app.MapControllers();

        app.Run();
    }
}
