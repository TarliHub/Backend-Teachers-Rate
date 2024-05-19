using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TeacherRate.Api.MapperProfiles;
using TeacherRate.Api.Services;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Storage;
using TeacherRate.Storage.Abstraction.Interfaces;
using TeacherRate.Storage.Repository;

namespace TeacherRate.Api;

public static class ServicesConfigurationExtension
{
    private static WebApplicationBuilder? _builder;

    public static IServiceCollection ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        return services
            .AddBuilder(builder)
            //.AddSession()
            .AddHttpContextAccessor()
            .AddCors()
            .AddAuthentication()
            .AddDbContext()
            .AddServices()
            .AddMapperProfile()
            .AddControllersWithSwagger();
    }

    public static IServiceCollection AddBuilder(this IServiceCollection services, WebApplicationBuilder builder)
    {
        _builder = builder;
        return services;
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using TeacherRateContext dbContext =
            scope.ServiceProvider.GetRequiredService<TeacherRateContext>();

        dbContext.Database.Migrate();
    }
    private static IServiceCollection AddMapperProfile(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new TeacherRateProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IRepository, GenericRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                    policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                );
        });
    }

    public static IServiceCollection AddSession(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        return services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(
                int.Parse(_builder?.Configuration.GetSection("AppSettings:ExpireTime").Value ?? "3600"));
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        _builder?.Configuration.GetSection("AppSettings:Token").Value ?? string.Empty)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        return services.AddDbContext<TeacherRateContext>(
            options => {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
                var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
                var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "teachers_rate_db";
                var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "user";
                var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password12345";
                options.UseNpgsql($"Host={dbHost}; Port={dbPort}; Database={dbName}; Username={dbUser}; Password={dbPassword};");
            });
    }

    public static IServiceCollection AddControllersWithSwagger(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        return services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
}