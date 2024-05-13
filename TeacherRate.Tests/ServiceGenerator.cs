using Microsoft.EntityFrameworkCore;
using TeacherRate.Api.Services;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Storage.Repository;
using TeacherRate.Storage;

namespace TeacherRate.Tests;

public static class ServiceGenerator
{
    public static IUserService GetUserService(string dbName)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TeacherRateContext>();
        optionsBuilder.UseLazyLoadingProxies()
            .UseInMemoryDatabase(dbName);

        var context = new TeacherRateContext(optionsBuilder.Options);
        var repository = new GenericRepository(context);
        var userService = new UserService(repository);
        return userService;
    }
}
