using TeacherRate.Domain.Models;

namespace TeacherRate.Tests;

public class TaskServiceTest
{
    [Fact]
    public async Task TaskService_AddTask_CorrectResult()
    {
        var task = new UserTask()
        {
            Approval = "link",
            Title = "title",
            Points = { 20 },
            PointsDescription = "description",
            Category = new TaskCategory() { Name = "category" }
        };
        var service = ServiceGenerator.GetTaskService("TaskServiceDbTest1");

        await service.AddTask(task);
        int count = service.GetTasks().Count();

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task TaskService_SendRequest_CorrectResult()
    {
        /*var request = new TeacherRequest()
        {
            ApprovalLink = "link",
            Description = "description",
            Points = 20,
            Reviewer = null,
            Task = new UserTask()
            {
                Approval = "link",
                Title = "title",
                Points = { 20 },
                PointsDescription = "description",
                Category = new TaskCategory() { Name = "category" }
            },
            Teacher = null,
        };*/
    }
}
