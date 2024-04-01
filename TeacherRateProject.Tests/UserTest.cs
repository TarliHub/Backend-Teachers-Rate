using AutoMapper;
using TeacherRateProject.Models;
using TeacherRateProject.Services;
using TeacherRateProject.Services.Interfaces;
using TeacherRateProject.Tests.Fakes;

namespace TeacherRateProject.Tests;

public class UserTest
{
    private readonly IUserService<User, int> _userService;

    public UserTest()
    {

        _userService = new UserServiceFake();
    }


    [Fact]
    public async void User_Rating_RatingCalculatesCorrectly()
    {
        var user = new User()
        {
            Name = "Test",
            LastName = "Test",
            Login = "login",
            Password = "qwerty",
            RegisteredAt = DateTime.Now,
            Role = Role.User,
            Tasks = new List<CompletedTask>()
        };
        var approve = new TaskApprove()
        {
            Title = "Test",
        };
        var category = new TaskCategory()
        {
            Title = "Test",
        };
        var task = new RatingTask()
        {
            Title = "Test",
            RatingPoints = 100,
            Category = category,
            Approve = approve
        };
        var completedTask = new CompletedTask()
        {
            Task = task,
            ActualRating = 100,
        };
        user.Tasks.Add(completedTask);

        var updatedUser = await _userService.Add(user);

        Assert.Equal(100, updatedUser.Rating);
    }
}