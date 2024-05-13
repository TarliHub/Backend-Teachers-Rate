using TeacherRate.Domain.Models;

namespace TeacherRate.Tests;

public class CredentialsTest
{
    [Fact]
    public async Task Credentials_SameEmails_ThrowException()
    {
        string email = "test@gmail.com";
        Teacher user1 = new Teacher() { 
            Email = email,
            Name = "Jhon",
            LastName = "Doe",
            MiddleName = "Jhonson",
            CreatedAt = DateTime.Now
        };
        Teacher user2 = new Teacher()
        {
            Email = email,
            Name = "Jhon",
            LastName = "Doe",
            MiddleName = "Jhonson",
            CreatedAt = DateTime.Now
        };
        var service = ServiceGenerator.GetUserService("CredsTestDB1");

        var exceptionHandler = async () =>
        {
            await service.AddUser(user1, "1234");
            await service.AddUser(user2, "4321");
            Assert.Fail("no exception was thrown");
        };

        await Assert.ThrowsAsync<ArgumentException>(exceptionHandler);
    }

    [Fact]
    public async Task Credentials_SamePasswords_CorrectResult()
    {
        Teacher user1 = new Teacher()
        {
            Email = "jhon@gmail.com",
            Name = "Jhon",
            LastName = "Doe",
            MiddleName = "Jhonson",
            CreatedAt = DateTime.Now
        };
        Teacher user2 = new Teacher()
        {
            Email = "jhon1@gmail.com",
            Name = "Jhon",
            LastName = "Doe",
            MiddleName = "Jhonson",
            CreatedAt = DateTime.Now
        };
        HeadTeacher user3 = new HeadTeacher()
        {
            Email = "jhon2@gmail.com",
            Name = "Jhon",
            LastName = "Doe",
            MiddleName = "Jhonson",
            CreatedAt = DateTime.Now,
            Teachers = new List<Teacher>{ user1, user2 }
        };
        var service = ServiceGenerator.GetUserService("CredsTestDB2");

        await service.AddUser(user3, "1234");

        var id = service.GetHeadTeachers().First().Id;

        var users = service.GetTeachers(id).ToList();

        Assert.Equal(2, users.Count);
    }
}