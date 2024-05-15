using Microsoft.EntityFrameworkCore;
using TeacherRate.Domain.Models;

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
        var userService = ServiceGenerator.GetUserService("UserServiceTestDB");

        foreach (var obj in Users)
        {
            HeadTeacher user = new HeadTeacher()
            {
                Name = (string)obj[0],
                LastName = (string)obj[1],
                MiddleName = (string)obj[2],
                Email = (string)obj[3],
                CommissionName = "",
                CreatedAt = DateTime.UtcNow,
            };
            await userService.AddUser(user, StrongPassword);
        }
        var users = await userService.GetHeadTeachers().ToListAsync();

        Assert.Equal(Users.Count, users.Count);
    }

    [Fact]
    public async Task UserService_DeleteEntity_CorrectResult()
    {
        var userService = ServiceGenerator.GetUserService("UserServiceTestDB1");
        HeadTeacher user = new HeadTeacher()
        {
            Email = "jhon@gmail.com",
            Name = "Jhon",
            LastName = "Doe",
            MiddleName = "Jhonson",
            CommissionName = "",
            CreatedAt = DateTime.Now
        };

        var userFromDb = await userService.AddUser(user, StrongPassword);
        await userService.RemoveUser(userFromDb.Id);

        Assert.Equal(0, userService.GetHeadTeachers().Count());
    }

    [Fact]
    public async Task UserService_DeleteEntity_DeleteWithIncorrectId()
    {
        var userService = ServiceGenerator.GetUserService("UserServiceTestDB1");
        
        var exceptionHandler = async () => await userService.RemoveUser(100);

        await Assert.ThrowsAsync<ArgumentException>(exceptionHandler);
    }
}
