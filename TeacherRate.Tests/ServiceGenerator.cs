using Microsoft.EntityFrameworkCore;
using TeacherRate.Api.Services;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Storage.Repository;
using TeacherRate.Storage;
using TeacherRate.Storage.Abstraction.Interfaces;

namespace TeacherRate.Tests;

public static class ServiceGenerator
{
    public static IUserService GetUserService(string dbName)
    {
        var userService = new UserService(GetInMemoryRepository(dbName));
        return userService;
    }

    public static ITaskService GetTaskService(string dbName)
    {
        var userService = new TaskService(GetInMemoryRepository(dbName));
        return userService;
    }

    private static IRepository GetInMemoryRepository(string dbName)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TeacherRateContext>();
        optionsBuilder.UseLazyLoadingProxies()
            .UseInMemoryDatabase(dbName);

        var context = new TeacherRateContext(optionsBuilder.Options);
        return new GenericRepository(context);
    }
}
