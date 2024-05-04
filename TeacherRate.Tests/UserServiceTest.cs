using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TeacherRate.Api.Services;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;
using TeacherRate.Storage;
using TeacherRate.Storage.Abstraction.Interfaces;
using TeacherRate.Storage.Repository;
using TeacherRate.Tests.Fakes;

namespace TeacherRate.Tests;


public class UserServiceTest
{
    private const string StrongPassword = "htR56!dGd56";

    public static List<object[]> Users = new List<object[]>
    {
        new object[]{ "Dima", "Hnidyi", "Dmytrovich", "dima@gmail.com", },
        new object[]{ "Misha", "Povroznik", "Mishovich", "misha@gmail.com" },
        new object[]{ "Pedro", "Petrik", "Pedrovich", "pedro@gmail.com" },
        new object[]{ "Andrew", "Drake", "Andrewvich", "drake@gmail.com" },
    };

    [Fact]
    public async Task HeadTeachers_AddEntity_CorrectValues()
    {
        var userService = GetUserService();

        foreach (var obj in Users)
        {
            HeadTeacher user = new HeadTeacher()
            {
                Name = (string)obj[0],
                LastName = (string)obj[1],
                MiddleName = (string)obj[2],
                Email = (string)obj[3],
                CreatedAt = DateTime.UtcNow,
            };
            await userService.AddUser(user, StrongPassword);
        }
        var users = await userService.GetHeadTeachers(0, 100);

        Assert.Equal(Users.Count, users.Count);
    }

    private IUserService GetUserService()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TeacherRateContext>();
        optionsBuilder.UseLazyLoadingProxies()
            .UseInMemoryDatabase("InMemoryDb");

        var context = new TeacherRateContext(optionsBuilder.Options);
        var repository = new GenericRepository(context);
        var userService = new UserService(repository);
        return userService;
    }
}
