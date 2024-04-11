using Microsoft.EntityFrameworkCore;
using TeacherRateProject.Data;
using TeacherRateProject.Data.UnitOfWork;
using TeacherRateProject.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddUserServices();
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<DataContext>(options =>
{
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
    var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "TeachersRate";
    var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "user";
    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password12345";
    options.UseNpgsql($"Host={dbHost}; Port={dbPort}; Database={dbName}; Username={dbUser}; Password={dbPassword};");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
