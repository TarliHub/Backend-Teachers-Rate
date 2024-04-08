using Microsoft.EntityFrameworkCore;
using TeacherRateProject.Data;
using TeacherRateProject.Data.UnitOfWork;
using TeacherRateProject.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddUserServices();
builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbName = Environment.GetEnvironmentVariable("DB_NAME");
    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
    options.UseSqlServer($"Server={dbHost},1433;Database={dbName};Integrated Security=True;");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
