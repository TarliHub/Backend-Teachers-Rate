using TeacherRateProject.DTOs;
using TeacherRateProject.Services.Interfaces;
using TeacherRateProject.Services;
using TeacherRateProject.Data.Repository.Interfaces;
using TeacherRateProject.Data.Repository.SqlRepository;

namespace TeacherRateProject.Helpers;

public static class CompositionRoot
{
    public static void AddUserServices(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService<UserDto, int>, UserService>();
    }
}
